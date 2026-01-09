using System.ComponentModel.DataAnnotations;

namespace RockSchool.WebApi.Models.Auth;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Range(0, 3)]
    public int RoleId { get; set; }
}
