using SocialMed.API.Reports.Domain.Models;
using SocialMed.API.Shared.Domain.Services.Communication;

namespace SocialMed.API.Reports.Domain.Services.Communication;

public class ReportResponse: BaseResponse<Report>
{
    public ReportResponse(string message) : base(message)
    {
    }

    public ReportResponse(Report resource) : base(resource)
    {
    } 
}