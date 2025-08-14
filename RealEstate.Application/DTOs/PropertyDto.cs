using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs
{
    public class PropertyDto
    {
        [Required] public string IdOwner { get; set; } = default!;
        [Required][StringLength(200)] public string Name { get; set; } = default!;
        [Required][StringLength(300)] public string Address { get; set; } = default!;
        [Range(0, double.MaxValue)] public decimal Price { get; set; }
        [Url] public string ImageUrl { get; set; } = default!;
    }
}
