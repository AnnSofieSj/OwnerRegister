
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class OwnersRepository(DataContext context) : Repo<OwnersEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<OwnersEntity> GetOneAsync(Expression<Func<OwnersEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Owners
                .Include(i => i.Horses)                
                .FirstOrDefaultAsync(expression);

            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }
    public override async Task<IEnumerable<OwnersEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Owners
                .Include(i => i.Horses)                
                .ToListAsync();
            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch { }
        return null!;
    }
}
