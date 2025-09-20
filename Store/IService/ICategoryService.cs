using Store.DTOs.Category;
using Store.Entity;

namespace Store.IService
{
    public interface ICategoryService 
    {
        Task AddCategoryAsync(CreateCategoryDto categoryDto);
        Task<List<CategoryDto>?> GetAllAsync();
        Task<Category> GetCategoryById(int id);
        Task UpdateCategory(int id, UpdateCategoryDto updateCategoryDto);
        Task DeleteCategory(int id);
        Task AddRangeCategory(List<CreateCategoryDto> categories);
        Task DeleteRangeCategory(List<int> ids);
    }
}
