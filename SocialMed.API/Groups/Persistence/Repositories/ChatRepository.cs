using Microsoft.EntityFrameworkCore;
using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Domain.Repositories;
using SocialMed.API.Shared.Persistence.Context;
using SocialMed.API.Shared.Persistence.Repositories;

namespace SocialMed.API.Groups.Persistence.Repositories;

public class ChatRepository :BaseRepository, IChatRepository
{
    public ChatRepository(AppDbContext context): base(context) 
    {
        
    }

    public async Task<IEnumerable<Chat>> ListAsync()//***
    {
        return await _context.Chats
            .Include(p=>p.User)
            .Include(p=>p.UserDestiny)
            .ToListAsync();
    }

    public async Task AddAsync(Chat chat)//***
    {
        await _context.Chats.AddAsync(chat);
    }

    public async Task<Chat> FindByIdAsync(int id)//****
    {
        return await _context.Chats
            .Include(p => p.User)
            .Include(p=>p.UserDestiny)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Chat>> FindByUserIdAsync(int id)//***
    {
        return await _context.Chats
            .Where(p => p.UserId == id)
            .Include(p => p.User)
            .Include(p =>p.UserDestiny)
            .ToListAsync();
    }

    public async Task<IEnumerable<Chat>> FindByUserDestinyIdAsync(int id)
    {
        return await _context.Chats
            .Where(p => p.UserDestinyId == id)
            .Include(p => p.User)
            .Include(p=>p.UserDestiny)
            .ToListAsync();
    }

    public async Task<Chat> FindByUserIdAndUserDestinyIdAsync(int userid, int userDestinyid)
    {
        return await _context.Chats
            .Where(p => p.UserId == userid).Where(p => p.UserDestinyId == userDestinyid)
            .Include(p => p.User)
            .Include(p => p.UserDestiny)
            .FirstOrDefaultAsync(p => p.UserId == userid && p.UserDestinyId==userDestinyid);////  revisar si funciona correctamente
    }
  
    public void Update(Chat chat)//***
    {
        _context.Chats.Update(chat);
    }

    public void Remove(Chat chat)//***
    {
        _context.Chats.Remove(chat);
    }
}