namespace Store.IService
{
    public interface IEmailService
    {
        Task SendMailAsync(string toEmail, string subject, string body);
        Task SendOtpToMailAsync(string toEmail, string otp);
    }
}
