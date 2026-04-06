using System.Security.Claims;
using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Services.Auth;

public interface ICurrentUserService
{
    Task<CurrentUserResponse> GetCurrentUserAsync(ClaimsPrincipal principal, CancellationToken cancellationToken = default);
}
