using System.ComponentModel.DataAnnotations;

namespace RockSchool.WebApi.Models;

public class RegisterUserRequestDto
{
    [Required]
    public string Login { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public int RoleId { get; set; } = 1;
}
