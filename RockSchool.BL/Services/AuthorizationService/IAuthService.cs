using RockSchool.BL.Models;

namespace RockSchool.BL.Services.AuthorizationService;

public interface IAuthService
{
    Task<LoginDetails> LoginAsync(string login, string password);
}