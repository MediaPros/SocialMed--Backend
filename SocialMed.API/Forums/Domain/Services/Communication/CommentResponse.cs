using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Shared.Domain.Services.Communication;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Domain.Services.Communication;

public class CommentResponse : BaseResponse<Comment>
{
    public CommentResponse(string message) : base(message)
    {
    }

    public CommentResponse(Comment resource) : base(resource)
    {
    }
}