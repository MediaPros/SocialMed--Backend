using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.SocialMedCenter.Resources;

public class SaveForumResource
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    [Required]
    public DateTime Date { get; set; }
    
    //relationsship
    
    [Required]
    public int UserId { get; set; }

}