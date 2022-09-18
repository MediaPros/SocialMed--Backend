using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    public int Age { get; set; }
    public string? Image { get; set; }
    [Required]
    [MaxLength(70)]
    public string Specialist { get; set; }
    [Required]
    public string WorkPlace { get; set; }
    public string? Biography { get; set; }
    [Required]
    [MaxLength(200)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}