using Store.IService;
using System.Net;
using System.Net.Mail;

namespace Store.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _emailFrom;
        private readonly string _emailPassword;
        private readonly string _displayName;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IConfiguration appsetting, ILogger<EmailService> logger)
        {
            _emailFrom = appsetting["EmailSettings:From"];
            _emailPassword = appsetting["EmailSettings:AppPassword"];
            _displayName = appsetting["EmailSettings:DisplayName"];
            _logger = logger;
        }
        public async Task SendMailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(_emailFrom, _emailPassword);
                    client.EnableSsl = true;
                    var mail = new MailMessage();
                    mail.From = new MailAddress(_emailFrom, _displayName);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    await client.SendMailAsync(mail);
                }
                _logger.LogInformation("Email has been send to {To}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi email tới {To}", toEmail);
                throw;
            }
        }
        public async Task SendOtpToMailAsync(string toEmail, string otp)
        {
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(_emailFrom, _emailPassword);
                    client.EnableSsl = true;
                    var mail = new MailMessage();
                    mail.From = new MailAddress(_emailFrom, _displayName);
                    mail.To.Add(toEmail);
                    mail.Subject = "Verify Email";
                    mail.Body = $@"
<div style='font-family: Arial, sans-serif; padding: 24px; border: 1px solid #cce4f8; border-radius: 10px; max-width: 520px; margin: auto; background-color: #ffffff;'>
    <h2 style='color: #89c5f0; text-align: center; margin-bottom: 20px;'>🔐 Mã Xác Thực OTP</h2>
    
    <p style='font-size: 16px; color: #333;'>Xin chào,</p>
    
    <p style='font-size: 15px; color: #444; line-height: 1.5;'>
        Cảm ơn bạn đã sử dụng dịch vụ <strong>Shop Support</strong>.  
        Vui lòng sử dụng mã OTP bên dưới để xác thực tài khoản của bạn:
    </p>

    <div style='font-size: 28px; font-weight: bold; color: #0b3761; text-align: center; margin: 24px 0; letter-spacing: 4px;'>
        {otp}
    </div>

    <p style='font-size: 14px; color: #555; line-height: 1.5;'>
        Mã OTP này có hiệu lực trong <strong>5 phút</strong>.  
        Để bảo mật, vui lòng không chia sẻ mã này với bất kỳ ai.
    </p>

    <hr style='margin: 30px 0; border: none; border-top: 1px dashed #cce4f8;' />

    <p style='font-size: 12px; color: #999; text-align: center;'>
        Trân trọng,<br/>
        <strong>Đội ngũ Shop Support</strong>
    </p>
</div>";
                    mail.IsBodyHtml = true;

                    await client.SendMailAsync(mail);
                }
                _logger.LogInformation("Email has been send to {To}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi email tới {To}", toEmail);
                throw;
            }
        }
    }
}
