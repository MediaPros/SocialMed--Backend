using SocialMed.API.Groups.Domain.Models;

namespace SocialMed.API.Groups.Domain.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> ListAsync();
    Task AddAsync(Message message);
    Task<Message> FindByIdAsync(int id);
    Task<IEnumerable<Message>> ListByChatIdAsync(int id);
    Task<IEnumerable<Message>> ListByChatIdAndUserIdAsync(int chatId,int userId);

    void Update(Message message);
    void Remove(Message message);
}