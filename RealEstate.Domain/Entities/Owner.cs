using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    public class Owner
    {
        [BsonId]
        public int IdOwner { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Photo { get; set; } = default!;
        public DateTime Birthday { get; set; }
    }
}