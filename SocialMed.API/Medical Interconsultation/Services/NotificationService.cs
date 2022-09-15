using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Repositories;
using SocialMed.API.Medical_Interconsultation.Domain.Services;
using SocialMed.API.Medical_Interconsultation.Domain.Services.Communication;
using SocialMed.API.Security.Domain.Repositories;
using SocialMed.API.Shared.Domain.Repositories;

namespace SocialMed.API.Medical_Interconsultation.Services;

public class NotificationService: INotificationService
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }


    public async Task<IEnumerable<Notification>> ListAsync()
    {
        return await _notificationRepository.ListAsync();
    }

    public async Task<IEnumerable<Notification>> ListByUserIdAsync(int userId)
    {
        return await _notificationRepository.ListByUserIdAsync(userId);
    }


    public async Task<NotificationResponse> SaveAsync(Notification notification)
    {
        if (notification.UserId == notification.ReferencesToUserId)
            return new NotificationResponse($"User and referenced user can't be the same person.");
        var user = await _userRepository.FindByIdAsync(notification.UserId);
        if (user == null)
            return new NotificationResponse($"User with id {notification.UserId} doesn't exist.");
        
        var referencedUser = await _userRepository.FindByIdAsync(notification.ReferencesToUserId);
        if (referencedUser == null)
            return new NotificationResponse($"Referenced user with id {notification.ReferencesToUserId} doesn't exist.");
        
        var existingNotificationWithTitleAndUser =
            await _notificationRepository.FindByTitleAndUserIdAndReferencesToUserId(notification.Title, notification.UserId, notification.ReferencesToUserId);
        if (existingNotificationWithTitleAndUser != null)
            return new NotificationResponse($"Notification {notification.Title} from user with id {notification.ReferencesToUserId} to user with id {notification.UserId} already exists.");
        
        try
        {
            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();
            return new NotificationResponse(notification);
        }
        catch (Exception e)
        {
            return new NotificationResponse($"An error occurred while saving notification: {e.Message}");
        }
    }
    
    public async Task<NotificationResponse> DeleteAsync(int id)
    {
        var existingNotification = await _notificationRepository.FindByIdAsync(id);
        if (existingNotification == null)
            return new NotificationResponse("Notification not found.");
        try
        {
            _notificationRepository.Remove(existingNotification);
            await _unitOfWork.CompleteAsync();
            return new NotificationResponse(existingNotification);
        }
        catch (Exception e)
        {
            return new NotificationResponse($"An error ocurred while deleting notification: {e.Message}");
        }
    }
}