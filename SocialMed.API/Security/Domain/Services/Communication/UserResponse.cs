using SocialMed.API.Security.Domain.Models;
using SocialMed.API.Shared.Domain.Services.Communication;

namespace SocialMed.API.Security.Domain.Services.Communication;

public class UserResponse : BaseResponse<User>
{
    public UserResponse(string message) : base(message)
    {
    }

    public UserResponse(User resource) : base(resource)
    {
    } 
}