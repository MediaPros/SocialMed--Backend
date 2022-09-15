using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Medical_Interconsultation.Resources;

public class SaveRecommendationResource
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int recommendationUserId { get; set; }
    [Required]
    public int recommendedUserId { get; set; }
}