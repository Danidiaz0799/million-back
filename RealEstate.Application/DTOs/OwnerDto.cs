using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs
{
    public class OwnerDto
    {
        [Required] public string Name { get; set; } = default!;
        [Required] public string Address { get; set; } = default!;
        [Url] public string Photo { get; set; } = default!;
        [Required] public DateTime Birthday { get; set; }
    }
}