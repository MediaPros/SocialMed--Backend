using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Shared.Domain.Services.Communication;

namespace SocialMed.API.Medical_Interconsultation.Domain.Services.Communication;

public class RecommendationResponse : BaseResponse<Recommendation>
{
    public RecommendationResponse(string message) : base(message)
    {
    }

    public RecommendationResponse(Recommendation resource) : base(resource)
    {
        
    }
}