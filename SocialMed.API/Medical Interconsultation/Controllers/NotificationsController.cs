using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Services;
using SocialMed.API.Medical_Interconsultation.Resources;
using SocialMed.API.Shared.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMed.API.Medical_Interconsultation.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class NotificationsController: ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public NotificationsController(INotificationService notificationService, IMapper mapper)
    {
        _notificationService = notificationService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NotificationResource>), 200)]
    public async Task<IEnumerable<NotificationResource>> GetAllAsync()
    {
        var notifications = await _notificationService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
        return resources;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(NotificationResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The notification was successfully created.", typeof(NotificationResource))]
    [SwaggerResponse(400, "The notification data is not valid.")]
    public async Task<IActionResult> PostAsync([FromBody] SaveNotificationResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var notification = _mapper.Map<SaveNotificationResource, Notification>(resource);
        var result = await _notificationService.SaveAsync(notification);

        if (!result.Success)
            return BadRequest(result.Message);

        var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);

        return Created(nameof(PostAsync), notificationResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _notificationService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var notificationsResource = _mapper.Map<Notification, NotificationResource>(result.Resource);

        return Ok(notificationsResource);
    }
}