using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Services.Auth;

public interface IAuthLoginService
{
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
