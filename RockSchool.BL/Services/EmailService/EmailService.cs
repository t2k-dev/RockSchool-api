using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace RockSchool.BL.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendPasswordEmailAsync(string toEmail, string login, string password)
    {
        var smtpHost = _configuration["Email:SmtpHost"];
        var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
        var senderEmail = _configuration["Email:SenderEmail"];
        var senderPassword = _configuration["Email:SenderPassword"];
        var senderName = _configuration["Email:SenderName"] ?? "Rock School";

        if (string.IsNullOrWhiteSpace(smtpHost) || string.IsNullOrWhiteSpace(senderEmail) || string.IsNullOrWhiteSpace(senderPassword))
        {
            throw new InvalidOperationException("Email configuration is missing. Please check appsettings.json");
        }

        var subject = "Учетные данные вашей учетной записи Rock School";
        var body = $@"
<html>
<body>
    <h2>Добро пожаловать в Rock School!</h2>
    <p>Ваша учетная запись успешно создана.</p>
    <p><strong>Логин:</strong> {login}</p>
    <p><strong>Пароль:</strong> {password}</p>
    <p>Пожалуйста, сохраните эту информацию в безопасности и измените пароль после первого входа в систему.</p>
    <br/>
    <p>С наилучшими пожеланиями,<br/>Команда Rock School</p>
</body>
</html>";

        using var smtpClient = new SmtpClient(smtpHost, smtpPort)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(senderEmail, senderPassword)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail, senderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
