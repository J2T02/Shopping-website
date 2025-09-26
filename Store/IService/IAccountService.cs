using Store.DTOs.Account;
using Store.DTOs.Email;
using Store.Entity;

namespace Store.IService
{
    public interface IAccountService
    {
        Task<AccountDto?> Login(string username); 
        Task Register(Account account);
        Task<AccountDto?> GetAccountByEmail(string email);
        Task AddEmailOTP(CreateEmailOTPDto createEmailOtp);
        Task<EmailOTPDto?> VerifyEmailOTP(VerifyOtpRequest request);
    }
}
