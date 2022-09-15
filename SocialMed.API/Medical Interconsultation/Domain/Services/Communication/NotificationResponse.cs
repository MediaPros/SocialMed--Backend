using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Shared.Domain.Services.Communication;

namespace SocialMed.API.Medical_Interconsultation.Domain.Services.Communication;

public class NotificationResponse: BaseResponse<Notification>
{
    public NotificationResponse(string message) : base(message)
    {
    }

    public NotificationResponse(Notification resource) : base(resource)
    {
        
    }
}