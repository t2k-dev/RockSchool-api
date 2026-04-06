using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Services.Auth;

public interface IUserAccountService
{
    Task<bool> ExistsByLoginAsync(string login, CancellationToken cancellationToken = default);
    Task<User?> FindByLoginAsync(string login, CancellationToken cancellationToken = default);
    Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Role> GetActiveRoleAsync(int roleId, CancellationToken cancellationToken = default);
}
