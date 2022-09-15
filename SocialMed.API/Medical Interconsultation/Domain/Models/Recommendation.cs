using SocialMed.API.Security.Domain.Models;

namespace SocialMed.API.Medical_Interconsultation.Domain.Models;

public class Recommendation
{
    public int Id { get; set; }
    public int recommendationUserId { get; set; }
    
    public User userRecommendation { get; set; }
    public int recommendedUserId { get; set; }

    public User userRecommended { get; set; }
}