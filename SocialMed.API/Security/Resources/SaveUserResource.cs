using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Security.Resources;

public class SaveUserResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public string Image { get; set; }
    [Required]
    [MaxLength(200)]
    public string Email { get; set; }
    [Required]
    [MaxLength(70)]
    public string Specialist { get; set; }
    [Required]
    public int Recommendation { get; set; }
    [Required]
    public string WorkPlace { get; set; }
    [Required]
    [MaxLength(80)]
    public string Password { get; set; }
    [Required]
    public string Biography { get; set; }
}