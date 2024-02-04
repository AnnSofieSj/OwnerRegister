

using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class HorsesService(HorsesRepository horsesRepository, BreedsRepository breedsRepository, OwnersRepository ownersRepository, AddressesRepository addressesRepository, BreedersRepository breedersRepository)
{
    private readonly HorsesRepository _horsesRepository = horsesRepository;
    private readonly BreedsRepository _breedsRepository = breedsRepository;
    private readonly OwnersRepository _ownersRepository = ownersRepository;
    private readonly AddressesRepository _addressesRepository = addressesRepository;
    private readonly BreedersRepository _breedersRepository = breedersRepository;


    public async Task<bool> AddHorseAsync(AddHorseDto addHorse)
    {
        try
        {
            var breedEntity = _breedsRepository.GetOneAsync(x => x.NameOfBreed ==  addHorse.NameOfBreed);
            if (breedEntity == null)
            {
                breedEntity = _breedsRepository.CreateAsync(new BreedsEntity { NameOfBreed = addHorse.NameOfBreed });
                return true;
            }

            var ownerEntity = _ownersRepository.GetOneAsync(x => x.FirstName == addHorse.OwnerFirstName && x.LastName == addHorse.OwnerLastName);
            if (ownerEntity == null)
            {
                ownerEntity = _ownersRepository.CreateAsync(new OwnersEntity {FirstName = addHorse.OwnerFirstName, LastName = addHorse.OwnerLastName});
                return true;
            }

            var breederEntity = _breedersRepository.GetOneAsync(x => x.FirstName == addHorse.BreederFirstName && x.LastName == addHorse.BreederLastName);
            if (breederEntity == null)
            {
                breederEntity = _breedersRepository.CreateAsync(new BreedersEntity { FirstName = addHorse.BreederFirstName, LastName = addHorse.BreederLastName });
                return true;
            }

            var addressEntity = _addressesRepository.GetOneAsync(x => x.City == addHorse.City);
            if (ownerEntity == null)
            {
                ownerEntity = _ownersRepository.CreateAsync(new OwnersEntity { FirstName = addHorse.OwnerFirstName, LastName = addHorse.OwnerLastName });
                return true;
            }

            var horseEntity = await _horsesRepository.CreateAsync(new HorsesEntity
            {
                HorseName = addHorse.HorseName,
                Gender = addHorse.Gender,
                YearOfBirth = addHorse.YearOfBirth,
                Color = addHorse.Color,
                Picture = addHorse.Picture,
            });

        }
        catch (Exception ex)
        {

        }
        return false;
    }


}
