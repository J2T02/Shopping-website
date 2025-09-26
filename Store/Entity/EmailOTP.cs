namespace Store.Entity
{
    public class EmailOTP : BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime ExpiredAt { get; set; }
        public bool IsUsed { get; set; } = false;
    }
}
