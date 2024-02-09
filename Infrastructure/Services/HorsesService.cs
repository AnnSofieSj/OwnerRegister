

using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class HorsesService(HorsesRepository horsesRepository, BreedsRepository breedsRepository, OwnersRepository ownersRepository, AddressesRepository addressesRepository, BreedersRepository breedersRepository, BreedsService breedsService, BreedersService breedersService, OwnersService ownersService, AddressesService addressesService)
{
    private readonly HorsesRepository _horsesRepository = horsesRepository;
    private readonly BreedsRepository _breedsRepository = breedsRepository;
    private readonly OwnersRepository _ownersRepository = ownersRepository;
    private readonly AddressesRepository _addressesRepository = addressesRepository;
    private readonly BreedersRepository _breedersRepository = breedersRepository;
    private readonly BreedsService _breedsService = breedsService;
    private readonly BreedersService _breedersService = breedersService;
    private readonly OwnersService _ownersService = ownersService;
    private readonly AddressesService _addressesService = addressesService;


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

    public async Task<IEnumerable<HorseDto>> GetAllHorsesAsync()
    {
        try
        {
            var horseEntities = await _horsesRepository.GetAllAsync();
            if (horseEntities != null)
            {
                var list = new List<HorseDto>();
                foreach (var horseEntity in horseEntities)
                    list.Add(new HorseDto
                    {
                        RegistrationId = horseEntity.RegistrationId,
                        HorseName = horseEntity.HorseName,
                        Gender = horseEntity.Gender,
                        YearOfBirth= horseEntity.YearOfBirth,
                        Color = horseEntity.Color,
                        Picture = horseEntity.Picture,

                        NameOfBreed = horseEntity.Breed.NameOfBreed,

                        BreederFirstName = horseEntity.Breeder.FirstName,
                        BreederLastName = horseEntity.Breeder.LastName,
                        BreederEmail = horseEntity.Breeder.Email,

                        OwnerFirstName = horseEntity.Owner.FirstName,
                        OwnerLastName = horseEntity.Owner.LastName,
                        OwnerEmail = horseEntity.Owner.Email,

                        Street = horseEntity.Address.Street,
                        StreetNr = horseEntity.Address.StreetNr,
                        PostalCode = horseEntity.Address.PostalCode,    
                        City = horseEntity.Address.City,
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


    public async Task<HorseDto> GetOneHorseAsync(Expression<Func<HorsesEntity, bool>> expression) 
    {
        try
        {
            var horseEntity = await _horsesRepository.GetOneAsync(expression);

            if (horseEntity != null)
            {
                var horseDto = new HorseDto
                {
                    RegistrationId = horseEntity.RegistrationId,
                    HorseName = horseEntity.HorseName,
                    Gender = horseEntity.Gender,
                    YearOfBirth = horseEntity.YearOfBirth,
                    Color = horseEntity.Color,
                    Picture = horseEntity.Picture,

                    NameOfBreed = horseEntity.Breed.NameOfBreed,

                    BreederFirstName = horseEntity.Breeder.FirstName,
                    BreederLastName = horseEntity.Breeder.LastName,
                    BreederEmail = horseEntity.Breeder.Email,

                    OwnerFirstName = horseEntity.Owner.FirstName,
                    OwnerLastName = horseEntity.Owner.LastName,
                    OwnerEmail = horseEntity.Owner.Email,

                    Street = horseEntity.Address.Street,
                    StreetNr = horseEntity.Address.StreetNr,
                    PostalCode = horseEntity.Address.PostalCode,
                    City = horseEntity.Address.City
                };              

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
                var breedEntity = await _breedsRepository.GetOneAsync(x => x.NameOfBreed == updatedHorse.NameOfBreed);
                breedEntity ??= await _breedsRepository.CreateAsync(new BreedsEntity { NameOfBreed = updatedHorse.NameOfBreed });
                var breedersEntity = await _breedersRepository.GetOneAsync(x => x.Email == updatedHorse.BreederEmail);
                breedersEntity ??= await _breedersRepository.CreateAsync(new BreedersEntity { FirstName = updatedHorse.BreederFirstName, LastName = updatedHorse.BreederLastName, Email = updatedHorse.BreederEmail });
                var ownersEntity = await _ownersRepository.GetOneAsync(x => x.Email == updatedHorse.OwnerEmail);
                ownersEntity ??= await _ownersRepository.CreateAsync(new OwnersEntity { FirstName = updatedHorse.OwnerFirstName, LastName = updatedHorse.OwnerLastName, Email = updatedHorse.OwnerEmail });
                var addressEntity = await _addressesRepository.GetOneAsync(x => x.Street == updatedHorse.Street && x.StreetNr == updatedHorse.StreetNr && x.PostalCode == updatedHorse.PostalCode && x.City == updatedHorse.City);
                addressEntity ??= await _addressesRepository.CreateAsync(new AddressesEntity { Street = updatedHorse.Street, StreetNr = updatedHorse.StreetNr, PostalCode = updatedHorse.PostalCode, City = updatedHorse.City });

                entity.HorseName = updatedHorse.HorseName;
                entity.Gender = updatedHorse.Gender;
                entity.YearOfBirth = updatedHorse.YearOfBirth;
                entity.Color = updatedHorse.Color;
                entity.Picture = updatedHorse.Picture;
                entity.BreedId = breedEntity.Id;
                entity.BreederId = breedersEntity.Id;
                entity.OwnerId = ownersEntity.Id;
                entity.AddressId = addressEntity.Id;

                var result = await _horsesRepository.UpdateAsync(entity);
                if (result != null)
                    return new HorseDto
                    {
                        RegistrationId = result.RegistrationId,
                        HorseName = result.HorseName,
                        Gender = result.Gender,
                        YearOfBirth = result.YearOfBirth,
                        Color = result.Color,
                        Picture = result.Picture,
                        NameOfBreed = result.Breed.NameOfBreed,
                        OwnerFirstName = result.Owner.FirstName,
                        OwnerLastName = result.Owner.LastName,
                        OwnerEmail = result.Owner.Email,
                        BreederFirstName = result.Breeder.FirstName,
                        BreederLastName = result.Breeder.LastName,
                        BreederEmail = result.Breeder.Email,
                        Street = result.Address.Street,
                        StreetNr = result.Address.StreetNr,
                        PostalCode = result.Address.PostalCode,
                        City = result.Address.City
                    };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }
}
