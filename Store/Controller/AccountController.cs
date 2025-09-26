using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.DTOs.Account;
using Store.DTOs.Common;
using Store.Entity;
using Store.Services;
using Store.Exceptions;
using Store.IService;
using Store.Helpers;
using Store.DTOs.Email;
using Store.Repositories;
using Store.DTOs.Customer;

namespace Store.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly JWTService _jwtService;
        private readonly IService.IEmailService _emailService;
        private readonly IRepository<EmailOTP> _emailRepository;
        private readonly ICustomerService _customerService;
        public AccountController(IAccountService accountService, IMapper mapper, JWTService jWTService, IRepository<EmailOTP> emailRepository, IService.IEmailService emailService, ICustomerService customerService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _jwtService = jWTService;
            _customerService = customerService;
            _emailRepository = emailRepository;
            _emailService = emailService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] CreateOTPDto createOtpDto)
        {
            var mail = createOtpDto.Email;
            if (!ModelState.IsValid)
            {
                return BadRequest($"Error: {ModelState}");
            }

            var checkEmail = await _accountService.GetAccountByEmail(mail);
            if (checkEmail != null)
            {
                return BadRequest(new BaseResponse { Message = $"Email is existed: {mail}" });
            }

            //OTP
            var otp = new Random().Next(100000, 999999).ToString();
            var createEmailOTP = new CreateEmailOTPDto
            {
                Code = otp,
                Email = mail,
                ExpiredAt = DateTime.UtcNow.AddMinutes(5)
            };
            //Save OTP
            await _accountService.AddEmailOTP(createEmailOTP);

            //Send OTP to Email
            await _emailService.SendOtpToMailAsync(mail, otp);
            return Ok(new BaseResponse { Message = "OTP has been send to your email!" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> VerifyEmail([FromBody] CreateAccountDto createAccountDto)
        {
            //Xác thực mail
            var verifyEmail = new VerifyOtpRequest { Email = createAccountDto.Email , Code = createAccountDto.Code};
            var otp = await _accountService.VerifyEmailOTP(verifyEmail);
            if (otp == null || otp.ExpiredAt < DateTime.UtcNow)
            {
                return BadRequest(new BaseResponse { Message = "OTP is wrong or expire!" });
            }
            otp.IsUsed = true;
            await _emailRepository.SaveChangeAsync();
            
            //Tạo account
            var passwordHash = PasswordHasher.HashPassword(createAccountDto.Password);
            if (passwordHash == null)
            {
                throw new NotFoundException("Password is required!");
            }
            var account = _mapper.Map<Account>(createAccountDto);
            account.PasswordHash = passwordHash;
            await _accountService.Register(account);
            return Ok(new BaseResponse { Message = "Register is success!" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Error:{ModelState}");
            }
            var account = await _accountService.Login(loginDto.EmailOrPhone);
            if (account == null)
            {
                throw new NotFoundException("User name is not found!");
            }
            var checkPassword = PasswordHasher.VerifyPassword(loginDto.Password, account.PasswordHash);
            if (checkPassword == false)
            {
                return BadRequest(new BaseResponse { Message = "Password is incorrect!" });
            }
            var token = _jwtService.GenerateToken(_mapper.Map<Account>(account));
            if (token == null) throw new Exception("Token is null");
            var userLogged = new UserLoggedDto
            { Token = token,
                Id = account.Id,
                PasswordHash = account.PasswordHash,
                DeviceId = account.DeviceId,
                Email = account.Email,
                Phone = account.Phone,
                Role = account.Role,
                UserName = account.UserName };
            //Check xem có customer chưa nếu chưa thì tạo
            
            return Ok(new BaseResponse { Data = userLogged, Message = "Login Successfull" });
        }

        
    }
}
