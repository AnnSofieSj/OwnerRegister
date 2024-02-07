

namespace Infrastructure.Dtos;

public class HorseDto
{
    public int RegistrationId { get; set; }
    public string HorseName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string YearOfBirth { get; set; } = null!;
    public string? Color { get; set; }


    public List<BreedsDto> Breeds { get; set; } = new List<BreedsDto>();

    //public BreedsDto? Breeds { get; set; }
    //public BreedersDto? Breeders { get; set; }
    //public OwnersDto? Owners { get; set; }
    //public AddressesDto? Addresses { get; set; }


    //public string NameOfBreed { get; set; } = null!;

    //public string OwnerFirstName { get; set; } = null!;
    //public string OwnerLastName { get; set; } = null!;
    //public string City { get; set; } = null!;

    //public string BreederFirstName { get; set; } = null!;
    //public string BreederLastName { get; set; } = null!;

}
