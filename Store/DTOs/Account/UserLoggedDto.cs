using Store.DTOs.Role;

namespace Store.DTOs.Account
{
    public class UserLoggedDto
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public RoleDto Role { get; set; }
        public string DeviceId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
