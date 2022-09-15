using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Services.Communication;

namespace SocialMed.API.Medical_Interconsultation.Domain.Services;

public interface INotificationService
{
    Task<IEnumerable<Notification>> ListAsync();
    Task<IEnumerable<Notification>> ListByUserIdAsync(int userId);
    Task<NotificationResponse> SaveAsync(Notification notification);
    Task<NotificationResponse> DeleteAsync(int id);
}