using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Database;
using Store.DTOs.Product;
using Store.Entity;
using Store.IService;
using Store.Repositories;
using Store.Exceptions;

namespace Store.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public ProductService(IRepository<Product> productRepository, IMapper mapper, ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task AddProductAsync(CreateProductDto createProductDto)
        {
            if (createProductDto == null) throw new ValidationException("Product created is required");
            if (string.IsNullOrEmpty(createProductDto.ProductName))
            {
                throw new ValidationException("Product Name is required");
            }
            if (!(createProductDto.Price > 0))
            {
                throw new ValidationException("Product price can not be 0 or lower");
            }
            if (!(createProductDto.Stock > 0))
            {
                throw new ValidationException("Product in Stock can not be 0 or lower");
            }
            var checkCategory = await _categoryService.GetCategoryById(createProductDto.CategoryId);
            var model = _mapper.Map<Product>(createProductDto);
            await _productRepository.AddAsync(model);
            await _productRepository.SaveChangeAsync();

        }

        public async Task AddRangeProduct(List<CreateProductDto> products)
        {
            foreach (var item in products)
            {
                await AddProductAsync(item);
            }
        }

        public async Task DeleteProduct(int id)
        {
            var model = await GetProductById(id);
            _productRepository.Delete(model);
            await _productRepository.SaveChangeAsync();
        }

        public async Task DeleteRangeProduct(List<int> ids)
        {
            if (ids.Count == 0 || ids is null) throw new ValidationException("Delete list is required");
            
            foreach (var item in ids)
            {
                await DeleteProduct(item);
            }
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var listModel = await _productRepository.Get().Include(x => x.Category).ToListAsync();
            var listDto = listModel.Select(x => _mapper.Map<ProductDto>(x)).ToList();
            return listDto;
        }

        public async Task<Product> GetProductById(int id)
        {
            var result = await _productRepository.Get().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new NotFoundException("Product not found!");
            return result;
        }

        public async Task UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null) throw new ValidationException("Product updated is required");
            if (string.IsNullOrEmpty(updateProductDto.ProductName))
            {
                throw new ValidationException("Product Name is required");
            }
            if (!(updateProductDto.Price > 0))
            {
                throw new ValidationException("Product price can not be 0 or lower");
            }
            if (!(updateProductDto.Stock > 0))
            {
                throw new ValidationException("Product in Stock can not be 0 or lower");
            }
            var category = await _categoryService.GetCategoryById(updateProductDto.CategoryId);
            var currentProduct = await GetProductById(id);
            currentProduct.ProductName = updateProductDto.ProductName;
            currentProduct.Price = updateProductDto.Price;
            currentProduct.Stock = updateProductDto.Stock;
            currentProduct.CategoryId = updateProductDto.CategoryId;
            _productRepository.Update(currentProduct);
            await _productRepository.SaveChangeAsync();
        }
    }
}
