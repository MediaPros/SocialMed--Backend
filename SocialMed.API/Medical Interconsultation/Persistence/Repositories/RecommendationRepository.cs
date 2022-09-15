using Microsoft.EntityFrameworkCore;
using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Repositories;
using SocialMed.API.Shared.Persistence.Context;
using SocialMed.API.Shared.Persistence.Repositories;

namespace SocialMed.API.Medical_Interconsultation.Persistence.Repositories;

public class RecommendationRepository: BaseRepository, IRecommendationRepository
{
    public RecommendationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Recommendation>> ListAsync()
    {
        return await _context.Recommendations.ToListAsync();
    }

    public async Task AddAsync(Recommendation recommendation)
    {
        await _context.AddAsync(recommendation);
    }

    public async Task<Recommendation> FindByIdAsync(int id)
    {
        return await _context.Recommendations.FindAsync(id);
    }

    public async Task<IEnumerable<Recommendation>> ListByRecommendationUserId(int userId)
    {
        return await _context.Recommendations
            .Where(p => p.recommendationUserId == userId)
            .Include(p => p.userRecommendation)
            .ToListAsync();
    }

    public async Task<IEnumerable<Recommendation>> ListByRecommendedUserId(int userId)
    {
        return await _context.Recommendations
            .Where(p => p.recommendedUserId == userId)
            .Include(p => p.userRecommended)
            .ToListAsync();
    }

    public void Remove(Recommendation recommendation)
    {
        _context.Recommendations.Remove(recommendation);
    }
}