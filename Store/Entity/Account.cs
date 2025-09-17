namespace Store.Entity
{
    public class Account : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public string DeviceId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Customer> Customers { get; set; } 
    }
}
