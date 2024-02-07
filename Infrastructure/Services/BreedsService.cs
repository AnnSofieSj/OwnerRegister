

using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;


namespace Infrastructure.Services;

public class BreedsService(BreedsRepository breedsRepository)
{

    private readonly BreedsRepository _breedsRepository = breedsRepository;

    public async Task<bool> CreateBreedAsync(string nameOfBreed)
    {
        try
        {
            if(!await _breedsRepository.ExistingAsync(x => x.NameOfBreed == nameOfBreed))
            {
                var breedEntity = await _breedsRepository.CreateAsync(new BreedsEntity { NameOfBreed = nameOfBreed });
                if(breedEntity != null)
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


    public async Task<BreedsDto> GetBreedAsync(Expression<Func<BreedsEntity, bool>> expression)
    {
        try
        {
            var breedsEntity = await _breedsRepository.GetOneAsync(expression);

            if (breedsEntity != null) 
            {
                var breedsDto = new BreedsDto
                {
                    Id = breedsEntity.Id,
                    NameOfBreed = breedsEntity.NameOfBreed
                };                   
               
                return breedsDto;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }

    public async Task<IEnumerable<BreedsDto>> GetAllBreedsAsync()
    {
        try
        {
            var breedsEntities = await _breedsRepository.GetAllAsync();

            if (breedsEntities != null)
            {
                var list = new List<BreedsDto>();
                foreach (var breedsEntity in breedsEntities)
                    list.Add(new BreedsDto
                    {
                        Id = breedsEntity.Id,
                        NameOfBreed = breedsEntity.NameOfBreed
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

    public async Task<BreedsDto> UpdateBreedAsync(BreedsDto updatedBreed)
    {
        try
        {
            var entity = await _breedsRepository.GetOneAsync(x => x.Id == updatedBreed.Id);
            if (entity != null)
            {
                entity.NameOfBreed = updatedBreed.NameOfBreed!;

                var result = await _breedsRepository.UpdateAsync(entity);
                if (result != null)
                    return new BreedsDto { Id = entity.Id, NameOfBreed = entity.NameOfBreed };

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }
}
