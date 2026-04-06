using System.Security.Cryptography;

namespace RockSchool.WebApi.Security.Passwords;

public class PasswordGenerator : IPasswordGenerator
{
    private const string Upper = "ABCDEFGHJKLMNPQRSTUVWXYZ";
    private const string Lower = "abcdefghijkmnopqrstuvwxyz";
    private const string Digits = "23456789";
    private const string All = Upper + Lower + Digits;

    public string Generate()
    {
        Span<char> password = stackalloc char[12];

        password[0] = Upper[RandomNumberGenerator.GetInt32(Upper.Length)];
        password[1] = Lower[RandomNumberGenerator.GetInt32(Lower.Length)];
        password[2] = Digits[RandomNumberGenerator.GetInt32(Digits.Length)];

        for (var i = 3; i < password.Length; i++)
            password[i] = All[RandomNumberGenerator.GetInt32(All.Length)];

        for (var i = password.Length - 1; i > 0; i--)
        {
            var swapIndex = RandomNumberGenerator.GetInt32(i + 1);
            (password[i], password[swapIndex]) = (password[swapIndex], password[i]);
        }

        return new string(password);
    }
}
