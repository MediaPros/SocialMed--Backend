using SocialMed.API.Groups.Domain.Models;

namespace SocialMed.API.Groups.Domain.Repositories;

public interface IChatRepository
{
    Task<IEnumerable<Chat>> ListAsync();
    Task AddAsync(Chat chat);
    Task<Chat> FindByIdAsync(int id);
    Task<IEnumerable<Chat>> FindByUserIdAsync(int id);
    Task<IEnumerable<Chat>> FindByUserDestinyIdAsync(int id);
    Task<Chat> FindByUserIdAndUserDestinyIdAsync(int userid, int userDestinyid);
    
    void Update(Chat chat);
    void Remove(Chat chat);
}