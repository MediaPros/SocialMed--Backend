using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Shared.Domain.Services.Communication;

namespace SocialMed.API.Groups.Domain.Services.Communication;

public class MessageResponse : BaseResponse<Message>
{
    public MessageResponse(string message) : base(message)
    {
    }

    public MessageResponse(Message resource) : base(resource)
    {
    }
}