using AutoMapper;
using Store.DTOs.Account;
using Store.DTOs.Email;
using Store.Entity;

namespace Store.MappingProfile
{
    public class AccountProfile : Profile
    {
        public AccountProfile() {
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<CreateAccountDto, Account>();
            CreateMap<EmailOTP, EmailOTPDto>().ReverseMap();
        }
    }
}
