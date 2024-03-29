﻿

using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class OwnersService(OwnersRepository ownersRepository)
{
    private readonly OwnersRepository _ownersRepository = ownersRepository;


    public async Task<bool> CreateOwnersAsync(OwnersDto owner)
    {
        try
        {
            if (!await _ownersRepository.ExistingAsync(x => x.Email == owner.Email))
            {
                var ownerEntity = await _ownersRepository.CreateAsync(new OwnersEntity
                {
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Email = owner.Email
                });

                if (ownerEntity != null)
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

    public async Task<OwnersDto> GetOwnerAsync(Expression<Func<OwnersEntity, bool>> expression)
    {
        try
        {
            var ownerEntity = await _ownersRepository.GetOneAsync(expression);
            if (ownerEntity != null)
            {
                var ownersDto = new OwnersDto
                {
                    Id = ownerEntity.Id,
                    FirstName = ownerEntity.FirstName,
                    LastName = ownerEntity.LastName,
                    Email = ownerEntity.Email
                };
                return ownersDto;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;

    }

    public async Task<IEnumerable<OwnersDto>> GetAllOwnersAsync()
    {
        try
        {
            var ownersEntities = await _ownersRepository.GetAllAsync();

            if (ownersEntities != null)
            {
                var list = new List<OwnersDto>();
                foreach (var ownersEntity in ownersEntities)
                    list.Add(new OwnersDto
                    {
                        Id = ownersEntity.Id,
                        FirstName = ownersEntity.FirstName,
                        LastName = ownersEntity.LastName,
                        Email = ownersEntity.Email
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

    public async Task<OwnersDto> UpdateOwnerAsync(OwnersDto updatedOwner)
    {
        try
        {
            var entity = await _ownersRepository.GetOneAsync(x => x.Id == updatedOwner.Id);
            if (entity != null)
            {
                entity.FirstName = updatedOwner.FirstName!;
                entity.LastName = updatedOwner.LastName!;
                entity.Email = updatedOwner.Email!;

                var result = await _ownersRepository.UpdateAsync(entity);
                if (result != null)
                    return new OwnersDto { Id = entity.Id, FirstName = entity.FirstName, LastName = entity.LastName, Email = entity.Email };

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return null!;
    }

    public async Task<bool> DeleteOwnerAsync(Expression<Func<OwnersEntity, bool>> expression)
    {
        try
        {
            var result = await _ownersRepository.DeleteAsync(expression);
            return result;
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error :: " + ex.Message);
        }
        return false;
    }
}
