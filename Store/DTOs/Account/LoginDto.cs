using System.ComponentModel.DataAnnotations;

namespace Store.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        public string EmailOrPhone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
