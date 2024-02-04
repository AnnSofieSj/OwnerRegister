

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class BreedsEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column (TypeName = "nvarchar(50)")]
    public string NameOfBreed { get; set; } = null!;

    public virtual ICollection<HorsesEntity> Horses { get; set; } = new List<HorsesEntity>();
}
