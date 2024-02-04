

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class AddHorseDto
{    
    public string HorseName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string YearOfBirth { get; set; } = null!;
    public string? Color { get; set; }
    public string? Picture { get; set; }

    public string NameOfBreed { get; set; } = null!;
    public string OwnerFirstName { get; set; } = null!; 
    public string OwnerLastName { get; set; } = null!;
    public string City { get; set; } = null!;


    public string BreederFirstName { get; set; } = null!;
    public string BreederLastName { get; set; } = null!;

}
