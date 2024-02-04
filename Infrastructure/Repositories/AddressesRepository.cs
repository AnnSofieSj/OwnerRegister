
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AddressesRepository(DataContext context) : Repo<AddressesEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<AddressesEntity> GetOneAsync(Expression<Func<AddressesEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Addresses
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
    public override async Task<IEnumerable<AddressesEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Addresses
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
