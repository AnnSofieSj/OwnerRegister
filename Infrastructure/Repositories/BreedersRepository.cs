
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BreedersRepository(DataContext context) : Repo<BreedersEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<BreedersEntity> GetOneAsync(Expression<Func<BreedersEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Breeders
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
    public override async Task<IEnumerable<BreedersEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Breeders
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
