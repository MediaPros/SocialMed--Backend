using Microsoft.EntityFrameworkCore;
using SocialMed.API.Reports.Domain.Models;
using SocialMed.API.Reports.Domain.Repositories;
using SocialMed.API.Shared.Persistence.Context;
using SocialMed.API.Shared.Persistence.Repositories;

namespace SocialMed.API.Reports.Persistence.Repositories;

public class ReportRepository : BaseRepository, IReportRepository
{
    public ReportRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Report>> ListAsync()
    {
        return await _context.Reports.
            Include(p => p.User).
            Include(p => p.Title).
            ToListAsync();
    }

    public async Task AddAsync(Report report)
    {
        await _context.Reports.AddAsync(report);
    }

    public async Task<Report> FindByIdAsync(int id)
    {
        return await _context.Reports
            .Include(p => p.Title)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Report>> ListByUserIdAsync(int id)
    {
        return await _context.Reports
            .Where(p => p.UserId == id)
            .Include(p => p.User)
            .ToListAsync();
    }
    
    public void Remove(Report report)
    {
        _context.Reports.Remove(report);
    }
}