
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BreedsRepository(DataContext context) : Repo<BreedsEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<BreedsEntity> GetOneAsync(Expression<Func<BreedsEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Breeds
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
    public override async Task<IEnumerable<BreedsEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Breeds
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
