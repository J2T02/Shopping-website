using Store.DTOs.Cart;
using Store.Entity;

namespace Store.IService
{
    public interface ICartService
    {
        Task<List<CartDto>?> GetAll();
        Task CreateCard(CreateCartDto createCardDto);
        Task AddProductsToCart(List<CreateCartItemDto> createCartItemDtos);
        Task<Cart> GetCartById(int cartId);
        Task<CartDto?> GetCartByCustomerId(int customerId);
        Task DeleteCart(int cartId);
        Task UpdateCart(int cartId, UpdateCartDto cart);
        Task<List<CartItemDto>?> GetCartItemsByCartId(int cartId);
    }
}
