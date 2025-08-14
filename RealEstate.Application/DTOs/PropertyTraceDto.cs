using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs
{
    public class PropertyTraceDto
    {
        public int Id { get; set; }
        [Required] public DateTime DateSale { get; set; }
        [Required] public string Name { get; set; } = default!;
        [Range(0, double.MaxValue)] public decimal Value { get; set; }
        [Range(0, double.MaxValue)] public decimal Tax { get; set; }
        [Required] public int IdProperty { get; set; }
    }
}