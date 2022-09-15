using Microsoft.EntityFrameworkCore;
using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Domain.Repositories;
using SocialMed.API.Shared.Persistence.Context;
using SocialMed.API.Shared.Persistence.Repositories;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Persistence.Repositories;

public class CommentRepository : BaseRepository, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Comment>> ListAsync()//**
    {
        return await _context.Comments.
            Include(p=>p.User).
            Include(p=>p.Forum).
            ToListAsync();
    }

    public async Task AddAsync(Comment comment)//**
    {
        await _context.Comments.AddAsync(comment);
    }

    public async Task<Comment> FindByIdAsync(int id)//***
    {
        return await _context.Comments
            .Include(p => p.Forum)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Comment>> ListByUserIdAsync(int id)//**
    {
        return await _context.Comments
            .Where(p => p.UserId == id)
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comment>> ListByForumIdAsync(int id)//***
    {
        return await _context.Comments
            .Where(p => p.ForumId == id)
            .Include(p => p.Forum)
            .ToListAsync();
    }


    public void Update(Comment comment)//***
    {
        _context.Comments.Update(comment);
    }

    public void Remove(Comment comment)//***
    {
        _context.Comments.Remove(comment);
    }
}