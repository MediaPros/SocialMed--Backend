using SocialMed.API.Reports.Domain.Models;
using SocialMed.API.Reports.Domain.Services.Communication;

namespace SocialMed.API.Reports.Domain.Services;

public interface IReportService
{
    Task<IEnumerable<Report>> ListAsync();
    Task<IEnumerable<Report>> ListByUserIdAsync(int userId);
    Task<ReportResponse> GetByIdAsync(int id);


    Task<ReportResponse> SaveAsync(Report report);
    Task<ReportResponse> DeleteAsync(int id);
}