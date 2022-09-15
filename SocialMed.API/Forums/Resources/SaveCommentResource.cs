using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Forums.Resources;

public class SaveCommentResource
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Content { get; set; }
    
    [Required]
    public int UserId { get; set; }
    [Required]
    public int ForumId { get; set; }
}