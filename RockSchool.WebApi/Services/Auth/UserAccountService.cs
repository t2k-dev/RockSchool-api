using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;

namespace RockSchool.WebApi.Services.Auth;

public class UserAccountService : IUserAccountService
{
    private readonly RockSchoolContext _context;
    private readonly ILookupNormalizer _lookupNormalizer;

    public UserAccountService(RockSchoolContext context, ILookupNormalizer lookupNormalizer)
    {
        _context = context;
        _lookupNormalizer = lookupNormalizer;
    }

    public Task<bool> ExistsByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        var normalizedLogin = NormalizeLogin(login);

        return _context.Users
            .AsNoTracking()
            .AnyAsync(candidate => candidate.NormalizedUserName == normalizedLogin, cancellationToken);
    }

    public Task<User?> FindByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        var normalizedLogin = NormalizeLogin(login);

        return _context.Users
            .Include(candidate => candidate.Role)
            .SingleOrDefaultAsync(candidate => candidate.NormalizedUserName == normalizedLogin, cancellationToken);
    }

    public Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _context.Users
            .Include(candidate => candidate.Role)
            .SingleOrDefaultAsync(candidate => candidate.Id == userId, cancellationToken);
    }

    public async Task<Role> GetActiveRoleAsync(int roleId, CancellationToken cancellationToken = default)
    {
        var role = await _context.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(candidate => candidate.RoleId == roleId && candidate.IsActive, cancellationToken);

        if (role is null)
            throw new InvalidOperationException("Selected role does not exist.");

        return role;
    }

    private string NormalizeLogin(string login)
    {
        var validatedLogin = AccountLoginNormalizer.NormalizeEmailLogin(login);

        return _lookupNormalizer.NormalizeName(validatedLogin)
               ?? throw new InvalidOperationException("Unable to normalize the login.");
    }
}
