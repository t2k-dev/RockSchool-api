using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace RockSchool.BL.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly EmailOptions _emailOptions;

    public EmailService(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendPasswordEmailAsync(string toEmail, string login, string password)
    {
        var subject = "Учетные данные вашей учетной записи Rock School";
        var body = $"""
                    <html>
                    <body>
                        <h2>Добро пожаловать в Rock School!</h2>
                        <p>Ваша учетная запись успешно создана.</p>
                        <p><strong>Логин:</strong> {WebUtility.HtmlEncode(login)}</p>
                        <p><strong>Пароль:</strong> {WebUtility.HtmlEncode(password)}</p>
                        <p>Пожалуйста, сохраните эту информацию в безопасности и измените пароль после первого входа в систему.</p>
                        <br/>
                        <p>С наилучшими пожеланиями,<br/>Команда Rock School</p>
                    </body>
                    </html>
                    """;

        using var smtpClient = new SmtpClient(_emailOptions.SmtpHost, _emailOptions.SmtpPort)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_emailOptions.SenderEmail, _emailOptions.SenderPassword)
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailOptions.SenderEmail, _emailOptions.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
