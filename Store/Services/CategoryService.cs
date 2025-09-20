using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.DTOs.Category;
using Store.Entity;
using Store.Exceptions;
using Store.IService;
using Store.Repositories;

namespace Store.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddCategoryAsync(CreateCategoryDto categoryDto)
        {
            if (categoryDto == null) throw new ValidationException("Category created is required!");
            if (string.IsNullOrEmpty(categoryDto.Name))
            {
                throw new ValidationException("Category Name is required");
            }
            var category = new Category { Name = categoryDto.Name, IsActive = true };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangeAsync();
        }

        public async Task AddRangeCategory(List<CreateCategoryDto> categories)
        {
            foreach (var item in categories)
            {
                await AddCategoryAsync(item);
            }
        }

        public async Task DeleteCategory(int id)
        {
            var model = await GetCategoryById(id);
            _categoryRepository.Delete(model);
            await _categoryRepository.SaveChangeAsync();
        }

        public async Task DeleteRangeCategory(List<int> ids)
        {
            foreach (var item in ids)
            {
                await DeleteCategory(item);
            }
        }

        public async Task<List<CategoryDto>?> GetAllAsync()
        {
            return await _categoryRepository.Get()
                .Select(x => new CategoryDto { Id = x.Id, Name = x.Name, IsActive = x.IsActive })
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var result = await _categoryRepository.Get().FirstOrDefaultAsync(x => x.Id == id);
            if(result == null)
            {
                throw new NotFoundException("Category not found!");
            }
            return result;
        }

        public async Task UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (string.IsNullOrEmpty(updateCategoryDto.Name))
            {
                throw new ValidationException("Category Name is required");
            }
            var currentModel = await GetCategoryById(id);
            if (currentModel == null)
            {
                throw new NotFoundException("Category not found");
            }
            currentModel.Name = updateCategoryDto.Name;
            currentModel.IsActive = updateCategoryDto.IsActive;
            _categoryRepository.Update(currentModel);
            await _categoryRepository.SaveChangeAsync();
        }

    }
}
