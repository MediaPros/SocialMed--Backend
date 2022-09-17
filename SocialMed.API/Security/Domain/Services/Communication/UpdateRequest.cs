using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Security.Domain.Services.Communication;

public class UpdateRequest
{
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    public int Age { get; set; }
    public string? Image { get; set; }
    [MaxLength(70)]
    public string Specialist { get; set; }
    public string WorkPlace { get; set; }
    public string? Biography { get; set; }
    [MaxLength(200)]
    public string Email { get; set; }
    public string Password { get; set; }
}