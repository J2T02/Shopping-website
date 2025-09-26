using AutoMapper;
using Store.DTOs.Cart;
using Store.Entity;
using Store.IService;
using Store.Repositories;
using Store.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Store.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IProductService _productService;
        private readonly IRepository<Customer> _customerRepository;
        public CartService(IRepository<Cart> cartRepository, IMapper mapper, IRepository<CartItem> cartItemRepository, IProductService productService, IRepository<Customer> customerRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
            _productService = productService;
            _customerRepository = customerRepository;
        }

        public async Task AddProductsToCart(List<CreateCartItemDto> createCartItemDtos)
        {
            if (createCartItemDtos.Count == 0) throw new ValidationException("Products add to cart is required!");
            foreach (var item in createCartItemDtos)
            {
                var product = await _productService.GetProductById(item.ProductId);
                if (product == null)
                {
                    throw new NotFoundException("Product not found");
                }
                if (item.Quantity < 0)
                {
                    throw new ValidationException("Quantity must be higher than 0");
                }
                var cartItem = _mapper.Map<CartItem>(item);
                cartItem.UnitPrice = product.Price;
                await _cartItemRepository.AddAsync(cartItem);
                await _cartItemRepository.SaveChangeAsync();
            }
           
        }

        public async Task CreateCard(CreateCartDto createCardDto)
        {
            var customer = _customerRepository.Get().FirstOrDefaultAsync(x=>x.Id == createCardDto.CustomerId);
            if(customer == null)
            {
                throw new NotFoundException("Customer not found!");
            }
            var cart = _mapper.Map<Cart>(createCardDto);
            cart.CreatedDate = DateTime.UtcNow;
            cart.UpdatedDate = DateTime.UtcNow;
            await _cartRepository.AddAsync(cart);
            await _cartRepository.SaveChangeAsync();
        }

        public async Task DeleteCart(int cartId)
        {
            var cart = await GetCartById(cartId);
            _cartRepository.Delete(cart);
            await _cartRepository.SaveChangeAsync();
        }

        public async Task<List<CartDto>?> GetAll()
        {
            var cartList = await _cartRepository.Get().ToListAsync();
            var cartlistDto = cartList.Select(x=>_mapper.Map<CartDto>(x)).ToList();
            return cartlistDto;
        }

        public async Task<CartDto?> GetCartByCustomerId(int customerId)
        {
            var cart = await _cartRepository.Get().FirstOrDefaultAsync(x=>x.CustomerId == customerId);
            var cartDto = _mapper.Map<CartDto>(cart);
            return cartDto;
        }

        public async Task<Cart> GetCartById(int cartId)
        {
            var cart = await _cartRepository.Get()
                .Include(x=>x.Customer).ThenInclude(x=>x.Account)
                .FirstOrDefaultAsync(x=>x.Id == cartId);
            if (cart == null) throw new NotFoundException("Cart not found!");
            return cart;
        }

        public async Task<List<CartItemDto>?> GetCartItemsByCartId(int cartId)
        {
            var cart = await GetCartById(cartId);
            var cartItems = _cartItemRepository.Get().Where(x=>x.CartId == cartId)
                .Include(x=>x.Cart).ThenInclude(x=>x.Customer).ThenInclude(x=>x.Account)
                .Include(x=>x.Product).ThenInclude(x=>x.Category).ToList();
            var cartItemsDto = cartItems.Select(x=>_mapper.Map<CartItemDto>(x)).ToList();
            return cartItemsDto;
        }

        public async Task UpdateCart(int cartId, UpdateCartDto updateCart)
        {
            var cart = await GetCartById(cartId);
            cart.UpdatedDate = updateCart.UpdatedDate;
            _cartRepository.Update(cart);
            await _cartRepository.SaveChangeAsync();
        }
    }
}
