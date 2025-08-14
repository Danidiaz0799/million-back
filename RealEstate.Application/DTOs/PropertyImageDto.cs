using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs
{
    public class PropertyImageDto
    {
        public int Id { get; set; }
        [Required] public int IdProperty { get; set; }
        [Required] public string File { get; set; } = default!;
        public bool Enabled { get; set; } = true;
    }
}