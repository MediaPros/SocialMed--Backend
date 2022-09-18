using AutoMapper;
using Org.BouncyCastle.Asn1.X509;
using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Resources;
using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Resources;
using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Resources;
using SocialMed.API.Reports.Domain.Models;
using SocialMed.API.Reports.Resources;
using SocialMed.API.Security.Domain.Models;
using SocialMed.API.Security.Domain.Services.Communication;
using SocialMed.API.Security.Resources;
using SocialMed.API.SocialMedCenter.Domain.Models;
using SocialMed.API.SocialMedCenter.Resources;

namespace SocialMed.API.Shared.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveChatResource, Chat>();
        CreateMap<SaveCommentResource, Comment>();
        CreateMap<SaveForumResource, Forum>();
        CreateMap<SaveMessageResource, Message>();
        CreateMap<SaveRatingResource, Rating>();
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(options => options.Condition(
                (source, Target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && 
                        string.IsNullOrEmpty((string) property)) return false;
                    return true;
                }));
        CreateMap<SaveNotificationResource, Notification>();
        CreateMap<SaveRecommendationResource, Recommendation>();

        CreateMap<SaveReportResource, Report>();
    }

}