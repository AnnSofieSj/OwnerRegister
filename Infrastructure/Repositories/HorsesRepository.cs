
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class HorsesRepository(DataContext context) : Repo<HorsesEntity>(context)
{
    private readonly DataContext _context = context;


    public override async Task<HorsesEntity> GetOneAsync(Expression<Func<HorsesEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Horses
                .Include(i => i.Breed)
                .Include(i => i.Owner)
                .Include(i => i.Address)
                .Include(i => i.Breeder)
                .FirstOrDefaultAsync(expression);

            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }
    public override async Task<IEnumerable<HorsesEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Horses
                .Include(i => i.Breed)
                .Include(i => i.Owner)
                .Include(i => i.Address)
                .Include(i => i.Breeder)
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
