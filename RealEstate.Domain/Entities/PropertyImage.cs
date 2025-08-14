using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    public class PropertyImage
    {
        [BsonId]
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; } = default!;
        public bool Enabled { get; set; } = true;
    }
}