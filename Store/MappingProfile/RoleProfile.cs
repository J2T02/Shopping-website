using AutoMapper;
using Store.DTOs.Role;
using Store.Entity;

namespace Store.MappingProfile
{
    public class RoleProfile : Profile
    {
        public RoleProfile() {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
