using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Repositories;
using SocialMed.API.Shared.Persistence.Context;
using SocialMed.API.Shared.Persistence.Repositories;

namespace SocialMed.API.Medical_Interconsultation.Persistence.Repositories;

public class NotificationRepository: BaseRepository, INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Notification>> ListAsync()
    {
        return await _context.Notifications.ToListAsync();
    }

    public async Task<IEnumerable<Notification>> ListByUserIdAsync(int userId)
    {
        return await _context.Notifications
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.AddAsync(notification);
    }

    public async Task<Notification> FindByIdAsync(int id)
    {
        return await _context.Notifications.FindAsync(id);
    }

    public async Task<Notification> FindByTitleAndUserId(string title, int userId)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(p => p.Title == title && p.UserId == userId);
    }

    public async Task<Notification> FindByTitleAndUserIdAndReferencesToUserId(string title, int userId, int referencesToUserId)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(p => p.Title == title && p.UserId == userId && p.ReferencesToUserId == referencesToUserId);
    }

    public void Remove(Notification notification)
    {
        _context.Notifications.Remove(notification);
    }
}