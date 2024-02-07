

using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class HorsesService(HorsesRepository horsesRepository, BreedsRepository breedsRepository, OwnersRepository ownersRepository, AddressesRepository addressesRepository, BreedersRepository breedersRepository, BreedsService breedsService)
{
    private readonly HorsesRepository _horsesRepository = horsesRepository;
    private readonly BreedsRepository _breedsRepository = breedsRepository;
    private readonly OwnersRepository _ownersRepository = ownersRepository;
    private readonly AddressesRepository _addressesRepository = addressesRepository;
    private readonly BreedersRepository _breedersRepository = breedersRepository;
    private readonly BreedsService _breedsService = breedsService;


    public async Task<bool> AddHorseAsync(AddHorseDto addHorse)
    {
        try
        {
            var breedEntity = await _breedsRepository.GetOneAsync(x => x.NameOfBreed == addHorse.NameOfBreed);
            breedEntity ??= await _breedsRepository.CreateAsync(new BreedsEntity { NameOfBreed = addHorse.NameOfBreed });

            var ownerEntity = await _ownersRepository.GetOneAsync(x => x.Email == addHorse.OwnerEmail);
            ownerEntity ??= await _ownersRepository.CreateAsync(new OwnersEntity { FirstName = addHorse.OwnerFirstName, LastName = addHorse.OwnerLastName, Email = addHorse.OwnerEmail });

            var breederEntity = await _breedersRepository.GetOneAsync(x => x.Email == addHorse.BreederEmail);
            breederEntity ??= await _breedersRepository.CreateAsync(new BreedersEntity { FirstName = addHorse.BreederFirstName, LastName = addHorse.BreederLastName, Email = addHorse.BreederEmail });

            var addressEntity = await _addressesRepository.GetOneAsync(x => x.Street == addHorse.Street && x.StreetNr == addHorse.StreetNr && x.PostalCode == addHorse.PostalCode && x.City == addHorse.City);
            addressEntity ??= await _addressesRepository.CreateAsync(new AddressesEntity { Street = addHorse.Street, StreetNr = addHorse.StreetNr, PostalCode = addHorse.PostalCode, City = addHorse.City });

            var horseEntity = await _horsesRepository.CreateAsync(new HorsesEntity
            {
                HorseName = addHorse.HorseName,
                Gender = addHorse.Gender,
                YearOfBirth = addHorse.YearOfBirth,
                Color = addHorse.Color,
                Picture = addHorse.Picture,
                OwnerId = ownerEntity.Id,
                BreedId = breedEntity.Id,
                BreederId = breederEntity.Id,
                AddressId = addressEntity.Id
            });

            if (horseEntity != null)
            {
                return true;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return false;
    }

    public async Task<HorseDto> GetOneHorseAsync(int registrationId) //Ej klar
    {
        try
        {
            var horseEntity = await _horsesRepository.GetOneAsync(x => x.RegistrationId == registrationId);

            if (horseEntity != null)
            {
                var horseDto = new HorseDto
                {
                    RegistrationId = horseEntity.RegistrationId,
                    HorseName = horseEntity.HorseName,
                    Gender = horseEntity.Gender,
                    YearOfBirth = horseEntity.YearOfBirth,
                    Color = horseEntity.Color,                                    
                   
                };


                foreach (var breeds in horseDto.Breeds) 
                {
                    horseDto.Breeds.Add(new BreedsDto
                    {
                        Id = breeds.Id,
                        NameOfBreed = breeds.NameOfBreed,
                    });               
                }
               

                return horseDto;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;


    }


    public async Task<HorseDto> UpdateHorseAsync(HorseDto updatedHorse)
    {
        try
        {
            var entity = await _horsesRepository.GetOneAsync(x => x.RegistrationId == updatedHorse.RegistrationId);
            if (entity != null)
            {
                var breedDto = await _breedsService.CreateBreedAsync(); //why????

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }
}
