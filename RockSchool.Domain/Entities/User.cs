using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RockSchool.Domain.Entities;

public class User : IdentityUser<Guid>
{
    [NotMapped]
    public Guid UserId => Id;

    [NotMapped]
    public string Login => UserName ?? string.Empty;

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public int RoleId { get; private set; }
    public Role Role { get; private set; } = null!;
    public bool IsActive { get; private set; }

    private User()
    {
    }

    public static User Create(string login, string firstName, string lastName, int roleId)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Login is required", nameof(login));
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));

        var normalizedLogin = login.Trim();
        var normalizedFirstName = firstName.Trim();
        var normalizedLastName = lastName.Trim();

        return new User
        {
            Id = Guid.NewGuid(),
            UserName = normalizedLogin,
            NormalizedUserName = normalizedLogin.ToUpperInvariant(),
            FirstName = normalizedFirstName,
            LastName = normalizedLastName,
            RoleId = roleId,
            IsActive = true,
            SecurityStamp = Guid.NewGuid().ToString("N"),
            ConcurrencyStamp = Guid.NewGuid().ToString("N")
        };
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    public void UpdatePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash is required", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
        SecurityStamp = Guid.NewGuid().ToString("N");
    }

    public void ChangeRole(int roleId)
    {
        RoleId = roleId;
    }

    public void Activate()
    {
        IsActive = true;
        LockoutEnabled = false;
        LockoutEnd = null;
    }

    public void Deactivate()
    {
        IsActive = false;
        LockoutEnabled = true;
        LockoutEnd = DateTimeOffset.MaxValue;
    }
}
