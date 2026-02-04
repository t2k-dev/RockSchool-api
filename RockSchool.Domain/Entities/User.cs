namespace RockSchool.Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }
    public int RoleId { get; private set; }
    public Role Role { get; private set; }
    public bool IsActive { get; private set; }

    private User() { }

    public static User Create(string login, string passwordHash, int roleId)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Login is required", nameof(login));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required", nameof(passwordHash));

        return new User
        {
            UserId = Guid.NewGuid(),
            Login = login,
            PasswordHash = passwordHash,
            RoleId = roleId,
            IsActive = true
        };
    }

    public void UpdatePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash is required", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
    }

    public void ChangeRole(int roleId)
    {
        RoleId = roleId;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
