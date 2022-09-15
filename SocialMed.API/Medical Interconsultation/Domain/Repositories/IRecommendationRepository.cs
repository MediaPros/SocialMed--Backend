using SocialMed.API.Medical_Interconsultation.Domain.Models;

namespace SocialMed.API.Medical_Interconsultation.Domain.Repositories;

public interface IRecommendationRepository
{
    Task<IEnumerable<Recommendation>> ListAsync();
    Task AddAsync(Recommendation recommendation);
    Task<Recommendation> FindByIdAsync(int id);
    Task<IEnumerable<Recommendation>> ListByRecommendationUserId(int userId);
    Task<IEnumerable<Recommendation>> ListByRecommendedUserId(int userId);
    void Remove(Recommendation recommendation);
}