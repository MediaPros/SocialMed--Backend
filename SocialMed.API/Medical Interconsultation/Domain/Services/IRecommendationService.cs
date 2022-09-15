using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Services.Communication;

namespace SocialMed.API.Medical_Interconsultation.Domain.Services;

public interface IRecommendationService
{
    Task<IEnumerable<Recommendation>> ListAsync();
    Task<RecommendationResponse> SaveAsync(Recommendation recommendation);
    Task<RecommendationResponse> DeleteAsync(int id);
}