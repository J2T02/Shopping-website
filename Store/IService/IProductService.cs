using Store.DTOs.Product;
using Store.Entity;

namespace Store.IService
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task AddProductAsync(CreateProductDto createProductDto);
        Task UpdateProduct(UpdateProductDto updateProductDto);
        Task DeleteProduct(int id);
    }
}
