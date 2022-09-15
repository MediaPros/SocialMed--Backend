using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Shared.Domain.Services.Communication;

namespace SocialMed.API.Forums.Domain.Services.Communication;

public class RatingResponse : BaseResponse<Rating>
{
    public RatingResponse(string message) : base(message)
    {
    }

    public RatingResponse(Rating resource) : base(resource)
    {
    }
}