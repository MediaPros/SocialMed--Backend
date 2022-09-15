using AutoMapper;
using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Resources;
using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Resources;
using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Resources;
using SocialMed.API.Security.Domain.Models;
using SocialMed.API.Security.Resources;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Shared.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Chat, ChatResource>();
        CreateMap<Comment, CommentResource>();
        CreateMap<Forum, ForumResource>();
        CreateMap<Message, MessageResource>();
        CreateMap<Rating, RatingResource>();
        CreateMap<User, UserResource>();
        CreateMap<Recommendation, RecommendationResource>();
        CreateMap<Notification, NotificationResource>();
        
    }
}