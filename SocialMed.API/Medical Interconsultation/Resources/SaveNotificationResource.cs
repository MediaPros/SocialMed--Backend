using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Medical_Interconsultation.Resources;

public class SaveNotificationResource
{
    [Required]
    [MaxLength(30)]
    public string Title { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public string ActionsCodes { get; set; }
    
    [Required]
    public int ReferencesToUserId { get; set; }
}