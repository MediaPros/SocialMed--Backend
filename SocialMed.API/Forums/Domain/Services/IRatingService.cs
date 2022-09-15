using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Domain.Services.Communication;

namespace SocialMed.API.Forums.Domain.Services;

public interface IRatingService
{
    Task<IEnumerable<Rating>> ListAsync();
    Task<IEnumerable<Rating>> ListByForumIdAsync(int forumId);
    Task<RatingResponse> SaveAsync(Rating rating);
    Task<RatingResponse> UpdateAsync(int id, Rating rating);
    Task<RatingResponse> DeleteAsync(int id);

}