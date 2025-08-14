namespace RealEstate.Domain.Entities
{
    public class Property
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal Price { get; set; }
        public string OwnerId { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}
