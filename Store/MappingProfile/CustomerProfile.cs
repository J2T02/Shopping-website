using AutoMapper;
using Store.DTOs.Customer;
using Store.Entity;

namespace Store.MappingProfile
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerDto, Customer>();
        }
    }
}
