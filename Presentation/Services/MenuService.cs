
using Infrastructure.Dtos;
using Infrastructure.Services;

namespace Presentation.Services;

public class MenuService(BreedsService breedsService)
{
    private readonly BreedsService _breedsService = breedsService;


    public async void AddNewBreed()
    {
        var breed = new BreedsDto();

        Console.WriteLine("Rasens namn : ");
        

        await _breedsService.CreateBreedAsync(breed.NameOfBreed = Console.ReadLine()!);

        Console.ReadKey();
    }
}
