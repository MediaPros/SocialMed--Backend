using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Domain.Repositories;

public interface IForumRepository
{
    Task<IEnumerable<Forum>> ListAsync();
    Task AddAsync(Forum forum);
    Task<Forum> FindByIdAsync(int id);
    Task<Forum> FindByTitleAsync(string title);
    Task<IEnumerable<Forum>> ListByUserIdAsync(int id);

    void Update(Forum forum);
    void Remove(Forum forum);
}