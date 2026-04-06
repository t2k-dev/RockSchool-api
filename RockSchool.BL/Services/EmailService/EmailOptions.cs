using System.ComponentModel.DataAnnotations;

namespace RockSchool.BL.Services.EmailService;

public class EmailOptions
{
    public const string SectionName = "Email";

    [Required]
    public string SmtpHost { get; set; } = string.Empty;

    [Range(1, 65535)]
    public int SmtpPort { get; set; } = 587;

    [Required]
    [EmailAddress]
    public string SenderEmail { get; set; } = string.Empty;

    [Required]
    public string SenderPassword { get; set; } = string.Empty;

    [Required]
    public string SenderName { get; set; } = "Rock School";
}
