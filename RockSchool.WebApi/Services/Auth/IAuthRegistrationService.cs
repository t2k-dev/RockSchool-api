using RockSchool.WebApi.Models;
using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Services.Auth;

public interface IAuthRegistrationService
{
    Task<RegisterResponse> RegisterAsync(RegisterUserRequestDto request, CancellationToken cancellationToken = default);
}
