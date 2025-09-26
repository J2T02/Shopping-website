namespace Store.DTOs.Email
{
    public class VerifyOtpRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }

    }
}
