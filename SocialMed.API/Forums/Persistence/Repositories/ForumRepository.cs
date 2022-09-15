using Microsoft.EntityFrameworkCore;
using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Domain.Repositories;
using SocialMed.API.Shared.Extensions;
using SocialMed.API.Shared.Persistence.Context;
using SocialMed.API.Shared.Persistence.Repositories;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Persistence.Repositories;

public class ForumRepository :BaseRepository, IForumRepository
{
    public ForumRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Forum>> ListAsync()//*****
    {
        return await _context.Forums.
            Include(p=>p.User).
            ToListAsync();
    }

    public async Task AddAsync(Forum forum)//******
    {
        await _context.Forums.AddAsync(forum);
    }

    public async Task<Forum> FindByIdAsync(int id)
    {
        return await _context.Forums
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Forum> FindByTitleAsync(string title)
    {
        return await _context.Forums
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Title == title);
    }

    public async Task<IEnumerable<Forum>> ListByUserIdAsync(int id)
    {
        return await _context.Forums
            .Where(p => p.UserId == id)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Forum forum)
    {
        _context.Forums.Update(forum);
    }

    public void Remove(Forum forum)
    {
        _context.Forums.Remove(forum);
    }
}