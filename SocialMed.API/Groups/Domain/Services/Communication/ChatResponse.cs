using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Shared.Domain.Services.Communication;

namespace SocialMed.API.Groups.Domain.Services.Communication;

public class ChatResponse : BaseResponse<Chat>
{
    public ChatResponse(string message) : base(message)
    {
    }

    public ChatResponse(Chat resource) : base(resource)
    {
    }
}