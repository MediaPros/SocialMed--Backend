using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Domain.Services.Communication;

namespace SocialMed.API.Groups.Domain.Services;

public interface IMessageService
{
    Task<IEnumerable<Message>> ListAsync();
    Task<IEnumerable<Message>> ListByChatIdAsync(int id);
    Task<MessageResponse> SaveAsync(Message message);
    Task<MessageResponse> UpdateAsync(int id, Message message);
    Task<MessageResponse> DeleteAsync(int id);

}