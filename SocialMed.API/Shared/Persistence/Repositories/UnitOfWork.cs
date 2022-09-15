using System.Threading.Tasks;
using SocialMed.API.Shared.Domain.Repositories;
using SocialMed.API.Shared.Persistence.Context;

namespace SocialMed.API.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}