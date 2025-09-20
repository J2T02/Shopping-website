using Store.DTOs.Product;
using Store.Entity;

namespace Store.IService
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task AddProductAsync(CreateProductDto createProductDto);
        Task UpdateProduct(int id, UpdateProductDto updateProductDto);
        Task DeleteProduct(int id);
        Task<Product> GetProductById(int id);
        Task AddRangeProduct(List<CreateProductDto> products);
        Task DeleteRangeProduct(List<int> ids);
    }
}
