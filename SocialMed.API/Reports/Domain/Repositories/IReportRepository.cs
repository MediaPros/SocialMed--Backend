using SocialMed.API.Reports.Domain.Models;

namespace SocialMed.API.Reports.Domain.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<Report>> ListAsync();
    Task AddAsync(Report report);
    Task<Report> FindByIdAsync(int id);
    Task<IEnumerable<Report>> ListByUserIdAsync(int id);
    
    void Remove(Report report);
}