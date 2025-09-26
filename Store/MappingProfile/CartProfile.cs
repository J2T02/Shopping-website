using AutoMapper;
using Store.DTOs.Cart;
using Store.Entity;

namespace Store.MappingProfile
{
    public class CartProfile : Profile
    {
        public CartProfile() {
            CreateMap<CreateCartDto, Cart>();
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CreateCartItemDto, CartItem>();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
