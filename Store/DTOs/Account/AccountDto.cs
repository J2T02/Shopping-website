using Store.DTOs.Role;
using Store.Entity;

namespace Store.DTOs.Account
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public RoleDto Role { get; set; }
        public string DeviceId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
