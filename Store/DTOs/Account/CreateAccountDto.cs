using System.ComponentModel.DataAnnotations;

namespace Store.DTOs.Account
{
    public class CreateAccountDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Mật khẩu cần từ 8 ký tự trở lên.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự, bao gồm cả chữ và số.")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Sai email.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(0(3|5|7|8|9))[0-9]{8}$", ErrorMessage = "Sai số điện thoại")]
        public string Phone { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
