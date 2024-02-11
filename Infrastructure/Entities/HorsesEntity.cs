

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class HorsesEntity
{
    [Key]
    public int RegistrationId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string HorseName { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string Gender { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string YearOfBirth { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string? Color { get; set; } 

    [Column(TypeName = "nvarchar(max)")]
    public string? Picture { get; set; } 

    [Required]
    [ForeignKey(nameof(BreedsEntity))]
    public int BreedId { get; set; }    

    [Required]
    [ForeignKey(nameof(BreedersEntity))]
    public int BreederId { get; set;}

    [Required]
    [ForeignKey(nameof(OwnersEntity))]
    public int OwnerId { get; set; }

    [Required]
    [ForeignKey(nameof(AddressesEntity))]
    public int AddressId { get; set; }


    public virtual BreedsEntity Breed { get; set; } = null!;

    public virtual BreedersEntity Breeder { get; set; } = null!;   

    public virtual OwnersEntity Owner { get; set; } = null!;

    public virtual AddressesEntity Address { get; set; } = null!;
}
