using System.ComponentModel.DataAnnotations;

namespace RockSchool.WebApi.Models.Account;

public class ChangePasswordRequest
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(NewPassword), ErrorMessage = "Password confirmation does not match the new password.")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
