

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos;

public class AddressesDto
{
    public int Id { get; set; }
    public string Street { get; set; } = null!;
    public string StreetNr { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
