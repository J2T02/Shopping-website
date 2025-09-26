using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.DTOs.Cart;
using Store.DTOs.Common;
using Store.Entity;
using Store.IService;
using Store.Repositories;

namespace Store.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCart()
        {
            var carts = await _cartService.GetAll();
            return Ok(new BaseResponse { Data = carts, Message = "Get Carts success!" });
        }

        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> GetCartByCustomerId([FromRoute] int customerId)
        {
            var carts = await _cartService.GetCartByCustomerId(customerId);
            if(carts == null)
            {
                return NotFound(new BaseResponse { Message = "Customer not found!" });
            }
            return Ok(new BaseResponse { Data = carts, Message = "Get Cart success!" });
        }

        [HttpGet("[action]/{cartId}")]
        public async Task<IActionResult> GetCartItemsByCartId([FromRoute] int cartId)
        {
            var cartItemsDto = await _cartService.GetCartItemsByCartId(cartId);
            return Ok(new BaseResponse { Data = cartItemsDto, Message = "Get CartItems success" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProductsToCart([FromBody] List<CreateCartItemDto> createCartItemDto)
        {
            await _cartService.AddProductsToCart(createCartItemDto);
            return Ok(new BaseResponse { Message = "Add Product to cart success!" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartDto createCartDto)
        {
            await _cartService.CreateCard(createCartDto);
            return Ok(new BaseResponse { Message = "Create cart success!" });
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] int id)
        {
            await _cartService.DeleteCart(id);
            return Ok(new BaseResponse { Message = "Delete cart success" });
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateCart([FromRoute] int id, [FromBody] UpdateCartDto updateCartDto)
        {
            await _cartService.UpdateCart(id, updateCartDto);
            return Ok(new BaseResponse { Message = "Update cart success" });
        }

        
    }
}
