using SocialMed.API.Medical_Interconsultation.Domain.Models;

namespace SocialMed.API.Medical_Interconsultation.Domain.Repositories;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> ListAsync();
    Task<IEnumerable<Notification>> ListByUserIdAsync(int userId);
    Task AddAsync(Notification notification);
    Task<Notification> FindByIdAsync(int id);
    Task<Notification> FindByTitleAndUserId(string title, int userId);
    Task<Notification> FindByTitleAndUserIdAndReferencesToUserId(string title, int userId, int referencesToUserId);
    void Remove(Notification notification);
}