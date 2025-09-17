using Store.DTOs.Category;
using Store.Entity;

namespace Store.IService
{
    public interface ICategoryService 
    {
        Task AddCategoryAsync(CreateCategoryDto categoryDto);
        Task<List<CategoryDto>?> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task UpdateCategory(int id, UpdateCategoryDto updateCategoryDto);
        Task DeleteCategory(int id);
    }
}
