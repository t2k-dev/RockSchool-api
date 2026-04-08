using System.Security.Claims;
using RockSchool.WebApi.Models.Account;

namespace RockSchool.WebApi.Services.Auth;

public interface IAuthPasswordService
{
    Task ChangePasswordAsync(
        ClaimsPrincipal principal,
        ChangePasswordRequest request,
        CancellationToken cancellationToken = default);
}
