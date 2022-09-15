using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Forums.Resources;

public class SaveRatingResource
{
    [Required]
    public int Rate { get; set; }
    //relationship
    [Required]
    public int ForumId { get; set; }
}