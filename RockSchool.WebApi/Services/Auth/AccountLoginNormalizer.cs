using System.Net.Mail;

namespace RockSchool.WebApi.Services.Auth;

internal static class AccountLoginNormalizer
{
    public static string NormalizeEmailLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Login is required.", nameof(login));

        var normalizedLogin = login.Trim();

        try
        {
            _ = new MailAddress(normalizedLogin);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Login must be a valid email address.", nameof(login));
        }

        return normalizedLogin;
    }
}
