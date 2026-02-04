namespace RockSchool.Domain.Entities;

public class Role
{
    public int RoleId { get; private set; }
    public string RoleName { get; private set; }
    public bool IsActive { get; private set; }

    private Role() { }

    public static Role Create(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            throw new ArgumentException("Role name is required", nameof(roleName));

        return new Role
        {
            RoleName = roleName,
            IsActive = true
        };
    }

    public void UpdateName(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            throw new ArgumentException("Role name is required", nameof(roleName));

        RoleName = roleName;
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
