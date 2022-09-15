using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Domain.Services.Communication;

namespace SocialMed.API.Groups.Domain.Services;

public interface IChatService
{
    Task<IEnumerable<Chat>> ListAsync();
    Task<IEnumerable<Chat>> ListByUserIdAsync(int userId);
    Task<IEnumerable<Chat>> ListByUserDestinyIdAsync(int userId);
    Task<Chat> GetByIdAsync(int id);
    Task<Chat> GetByUserIdAndUserDestinyIdAsync(int userId, int userDestinyId);
    Task<ChatResponse> SaveAsync(Chat chat);
    Task<ChatResponse> UpdateAsync(int id, Chat chat);
    Task<ChatResponse> DeleteAsync(int id);
    
}