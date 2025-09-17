using AutoMapper;
using Store.DTOs.Category;
using Store.Entity;

namespace Store.MappingProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
