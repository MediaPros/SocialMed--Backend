using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Services;
using SocialMed.API.Medical_Interconsultation.Resources;

namespace SocialMed.API.Medical_Interconsultation.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/notifications")]
public class UserNotificationsController
{
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public UserNotificationsController(INotificationService notificationService, IMapper mapper)
    {
        _notificationService = notificationService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NotificationResource>), 200)]
    public async Task<IEnumerable<NotificationResource>> GetNotificationsByUserIdAsync(int userId)
    {
        var notifications = await _notificationService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
        return resources;
    }
}