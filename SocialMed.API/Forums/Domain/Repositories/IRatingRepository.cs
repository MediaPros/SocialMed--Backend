using SocialMed.API.Forums.Domain.Models;

namespace SocialMed.API.Forums.Domain.Repositories;

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> ListAsync();
    Task AddAsync(Rating rating);
    Task<Rating> FindByIdAsync(int id);
    Task<IEnumerable<Rating>> ListByForumIdAsync(int id);
    void Update(Rating rating);
    void Remove(Rating rating);
}