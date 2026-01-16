namespace RockSchool.BL.Services.EmailService;

public interface IEmailService
{
    Task SendPasswordEmailAsync(string toEmail, string login, string password);
}
