using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.DTOs.Account;
using Store.DTOs.Email;
using Store.Entity;
using Store.IService;
using Store.Repositories;
using Store.Exceptions;

namespace Store.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<EmailOTP> _emailOtpRepository;
        private readonly IRepository<Role> _roleRepository;
        public AccountService(IRepository<Account> accountRepository, IMapper mapper, IRepository<EmailOTP> emailOtpRepository, IRepository<Role> roleRepository )
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _emailOtpRepository = emailOtpRepository;
            _roleRepository = roleRepository;
        }

        public async Task AddEmailOTP(CreateEmailOTPDto createEmailOtp)
        {
            if (string.IsNullOrEmpty(createEmailOtp.Email))
            {
                throw new ValidationException("Email is required!");
            }
            else if (string.IsNullOrEmpty(createEmailOtp.Code))
            {
                throw new ValidationException("OTP code is required!");
            }
            var emailOtp = new EmailOTP
            {
                Email = createEmailOtp.Email,
                Code = createEmailOtp.Code,
                ExpiredAt = createEmailOtp.ExpiredAt,
                IsUsed = false,
            };
            await _emailOtpRepository.AddAsync(emailOtp);
            await _emailOtpRepository.SaveChangeAsync();
        }

        public async Task<AccountDto?> GetAccountByEmail(string email)
        {
            var account = await _accountRepository.Get().FirstOrDefaultAsync(x=>x.Email == email);
            return _mapper.Map<AccountDto?>(account);
        }

        public async Task<AccountDto?> Login(string emailOrPhone)
        {
            var account = await _accountRepository.Get()
                .Include(x=> x.Role)
                .Include(x => x.Customers)
                .FirstOrDefaultAsync(x => x.Email == emailOrPhone
                || x.Phone == emailOrPhone)
                ;
           
            return _mapper.Map<AccountDto>(account);
        }

        public async Task Register(Account account)
        {
            if (string.IsNullOrEmpty(account.UserName))
            {
                throw new Exceptions.ValidationException("Username is required!");
            }
            else if (string.IsNullOrEmpty(account.PasswordHash))
            {
                throw new Exceptions.ValidationException("Password is required!");
            }
            else if (string.IsNullOrEmpty(account.Phone))
            {
                throw new Exceptions.ValidationException("Phone is required!");
            }
            else if (string.IsNullOrEmpty(account.Email))
            {
                throw new Exceptions.ValidationException("Email is required!");
            }
            account.RoleId = 3;
            account.DeviceId = Guid.NewGuid().ToString();

            await _accountRepository.AddAsync(account);
            await _accountRepository.SaveChangeAsync();
        }

        public async Task<EmailOTPDto?> VerifyEmailOTP(VerifyOtpRequest request)
        {
            var model = await _emailOtpRepository.Get().FirstOrDefaultAsync(x => x.Email == request.Email && x.Code == request.Code && x.IsUsed == false);
            return _mapper.Map<EmailOTPDto?>(model);
        }
    }
}
