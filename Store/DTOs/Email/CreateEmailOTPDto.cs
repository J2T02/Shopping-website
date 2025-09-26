namespace Store.DTOs.Email
{
    public class CreateEmailOTPDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
