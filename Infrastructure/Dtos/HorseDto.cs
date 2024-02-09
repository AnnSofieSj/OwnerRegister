

namespace Infrastructure.Dtos;

public class HorseDto
{
    public int RegistrationId { get; set; }
    public string HorseName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string YearOfBirth { get; set; } = null!;
    public string? Color { get; set; }
    public string? Picture { get; set; }

    public string NameOfBreed { get; set; } = null!;

    public string BreederFirstName { get; set; } = null!;
    public string BreederLastName { get; set; } = null!;
    public string BreederEmail { get; set; } = null!;

    public string OwnerFirstName { get; set; } = null!;
    public string OwnerLastName { get; set; } = null!;
    public string OwnerEmail { get; set; } = null!;

    public string Street { get; set; } = null!;
    public string StreetNr { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;



}
