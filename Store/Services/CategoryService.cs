using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.DTOs.Category;
using Store.Entity;
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
            if (string.IsNullOrEmpty(categoryDto.Name))
            {
                throw new Exception("Category Name is required");
            }
            var category = new Category { Name = categoryDto.Name, IsActive = true };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangeAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var model = await GetByIdAsync(id);
            var dto = _mapper.Map<Category>(model);
            _categoryRepository.Delete(dto);
            await _categoryRepository.SaveChangeAsync();
        }

        public async Task<List<CategoryDto>?> GetAllAsync()
        {
            return await _categoryRepository.Get()
                .Select(x => new CategoryDto { Id = x.Id, Name = x.Name, IsActive = x.IsActive })
                .ToListAsync();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            return await _categoryRepository.Get()
                .Select(x => new CategoryDto { Id = x.Id, Name = x.Name, IsActive = x.IsActive })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (string.IsNullOrEmpty(updateCategoryDto.Name))
            {
                throw new Exception("Category Name is required");
            }
            var currentModel = _mapper.Map<Category>(await GetByIdAsync(id));
            if (currentModel == null)
            {
                throw new Exception("Category not found");
            }
            var updateModel = _mapper.Map<Category>(updateCategoryDto);

            currentModel.Name = updateModel.Name;
            currentModel.IsActive = updateModel.IsActive;
            _categoryRepository.Update(currentModel);
            await _categoryRepository.SaveChangeAsync();
        }

    }
}
