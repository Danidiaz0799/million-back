using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    public class Property
    {
        [BsonId]
        public int IdProperty { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = default!;
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }
}
