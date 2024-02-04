
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AddressesEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Street { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string StreetNr { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string PostalCode { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

    public virtual ICollection<HorsesEntity> Horses { get; set; } = new List<HorsesEntity>();

   

  
}
