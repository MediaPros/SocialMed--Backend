using Microsoft.EntityFrameworkCore;
using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Domain.Repositories;
using SocialMed.API.Shared.Persistence.Context;
using SocialMed.API.Shared.Persistence.Repositories;

namespace SocialMed.API.Forums.Persistence.Repositories;

public class RatingRepository : BaseRepository, IRatingRepository
{
    public RatingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Rating>> ListAsync()
    {
        return await _context.Ratings.
            Include(p=>p.Forum).
            ToListAsync();
    }

    public async Task AddAsync(Rating rating)
    {
        await _context.Ratings.AddAsync(rating);
    }

    public async Task<Rating> FindByIdAsync(int id)
    {
        return await _context.Ratings
            .Include(p => p.Forum)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Rating>> ListByForumIdAsync(int id)
    {
        return await _context.Ratings
            .Where(p => p.ForumId == id)
            .Include(p => p.Forum)
            .ToListAsync();
    }

    public void Update(Rating rating)
    {
        _context.Ratings.Update(rating);
    }

    public void Remove(Rating rating)
    {
        _context.Ratings.Remove(rating);
    }
}