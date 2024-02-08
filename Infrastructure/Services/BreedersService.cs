

using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class BreedersService(BreedersRepository breedersRepository)
{
    private readonly BreedersRepository _breedersRepository = breedersRepository;


    public async Task<bool> CreateBreederAsync(BreedersDto breeder)
    {
        try
        {
            if (! await _breedersRepository.ExistingAsync(x => x.Email == breeder.Email))
            {
                var breederEntity = await _breedersRepository.CreateAsync(new BreedersEntity 
                { 
                    FirstName = breeder.FirstName,
                    LastName = breeder.LastName,
                    Email = breeder.Email     
                });

                if (breederEntity != null)
                {
                    return true;
                }
            }               

        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return false;
    }

    public async Task<BreedersDto> GetBreederAsync(Expression<Func<BreedersEntity, bool>> expression)
    {
        try
        {
            var breedersEntity = await _breedersRepository.GetOneAsync(expression);
            if (breedersEntity != null) 
            { 
                var breedersDto = new BreedersDto
                {
                    Id = breedersEntity.Id, 
                    FirstName = breedersEntity.FirstName, 
                    LastName = breedersEntity.LastName, 
                    Email = breedersEntity.Email
                };
                return breedersDto;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }

    public async Task<IEnumerable<BreedersDto>> GetAllBreedersAsync()
    {
        try
        {
            var breedersEntities = await _breedersRepository.GetAllAsync();

            if (breedersEntities != null)
            {
                var list = new List<BreedersDto>();
                foreach (var breedersEntity in breedersEntities)
                    list.Add(new BreedersDto
                    {
                        Id = breedersEntity.Id,
                        FirstName = breedersEntity.FirstName,
                        LastName = breedersEntity.LastName,
                        Email = breedersEntity.Email
                    });
                    return list;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }

    public async Task<BreedersDto> UpdateBreederAsync(BreedersDto updatedBreeder)
    {
        try
        {
            var entity = await _breedersRepository.GetOneAsync(x => x.Id == updatedBreeder.Id);
            if (entity != null)
            {
                entity.FirstName = updatedBreeder.FirstName!;
                entity.LastName = updatedBreeder.LastName!;
                entity.Email = updatedBreeder.Email!;

                var result = await _breedersRepository.UpdateAsync(entity);
                if (result != null)
                    return new BreedersDto { Id = entity.Id, FirstName = entity.FirstName, LastName = entity.LastName, Email = entity.Email };

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }

    public async Task<bool> DeleteBreederAsync(Expression<Func<BreedersEntity, bool>> expression)
    {
        try
        {
            var result = await _breedersRepository.DeleteAsync(expression);
            return result;
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return false;
    }

}
