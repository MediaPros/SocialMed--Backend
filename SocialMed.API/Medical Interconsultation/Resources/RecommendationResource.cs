using SocialMed.API.Security.Domain.Models;
using SocialMed.API.Security.Resources;

namespace SocialMed.API.Medical_Interconsultation.Resources;

public class RecommendationResource
{
    public int Id { get; set; }
    public int recommendationUserId { get; set; }
    public int recommendedUserId { get; set; }
    
    public UserResource userRecommendation { get; set; }
    public UserResource userRecommended { get; set; }
}