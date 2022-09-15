using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Groups.Resources;

public class SaveChatResource
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int UserDestinyId { get; set; }
    
}