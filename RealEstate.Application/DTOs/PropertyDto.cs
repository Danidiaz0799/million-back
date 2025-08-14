using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = default!;
        [Required][StringLength(300)] public string Address { get; set; } = default!;
        [Range(0, double.MaxValue)] public decimal Price { get; set; }
        [Required] public string CodeInternal { get; set; } = default!;
        [Range(1800, 9999)] public int Year { get; set; }
        [Required] public int IdOwner { get; set; }
    }
}
