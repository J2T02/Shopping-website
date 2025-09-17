using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Database;
using Store.DTOs.Product;
using Store.Entity;
using Store.IService;
using Store.Repositories;

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
            if (string.IsNullOrEmpty(createProductDto.ProductName))
            {
                throw new Exception("Product Name is required");
            }
            if(!(createProductDto.Price > 0))
            {
                throw new Exception("Product price can not be 0 or lower");
            }
            if (!(createProductDto.Stock > 0))
            {
                throw new Exception("Product in Stock can not be 0 or lower");
            }
            var checkCategory = await _categoryService.GetByIdAsync(createProductDto.CategoryId);
            if (checkCategory == null)
            {
                throw new Exception("Category is not found");
            }
            try
            {
                var model = _mapper.Map<Product>(createProductDto);
                await _productRepository.AddAsync(model);
                await _productRepository.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Save product failed: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var listModel = await _productRepository.Get().Include(x=>x.Category).ToListAsync();
            var listDto = listModel.Select(x=> _mapper.Map<ProductDto>(x)).ToList();
            return listDto;
        }

        public Task UpdateProduct(UpdateProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
