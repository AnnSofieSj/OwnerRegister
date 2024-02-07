

using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class AddressesService(AddressesRepository addressesrepository)
{
    private readonly AddressesRepository _addressesrepository = addressesrepository;


    public async Task<bool> CreateAddressAsync(AddressesDto address)
    {
        try
        {
            if (!await _addressesrepository.ExistingAsync(x => x.Street == address.Street && x.StreetNr == address.StreetNr && x.PostalCode == address.PostalCode))         //Id? eller annat söksätt? Finns den inte så finns ju inget id än
            {
                var addressEntity = await _addressesrepository.CreateAsync(new AddressesEntity
                {
                    Street = address.Street,                
                    StreetNr = address.StreetNr,
                    PostalCode = address.PostalCode,
                    City = address.City                  
                });

                if (addressEntity != null)
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

    public async Task<AddressesDto> GetAddressAsync(Expression<Func<AddressesEntity, bool>> expression)
    {
        try
        {
            var addressEntity = await _addressesrepository.GetOneAsync(expression);
            if (addressEntity != null)
            {
                var addressesDto = new AddressesDto
                {
                    Id = addressEntity.Id,
                    Street = addressEntity.Street,
                    StreetNr = addressEntity.StreetNr,
                    PostalCode = addressEntity.PostalCode,
                    City = addressEntity.City,
                };
                return addressesDto;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;
    }

    public async Task<AddressesDto> UpdateAddressAsync(AddressesDto updatedAddress)
    {
        try
        {
            var entity = await _addressesrepository.GetOneAsync(x => x.Id == updatedAddress.Id);
            if (entity != null)
            {
                entity.Street = updatedAddress.Street!;
                entity.StreetNr = updatedAddress.StreetNr!;
                entity.PostalCode = updatedAddress.PostalCode!;
                entity.City = updatedAddress.City!;

                var result = await _addressesrepository.UpdateAsync(entity);
                if (result != null)
                    return new AddressesDto { Id = entity.Id, Street = entity.Street, StreetNr = entity.StreetNr, PostalCode = entity.PostalCode, City = entity.City };

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }

}
