using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    public class PropertyTrace
    {
        [BsonId]
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = default!;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }
    }
}