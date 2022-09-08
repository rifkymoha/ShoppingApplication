using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shoppingapplication.Models
{
    //[Serializable, BsonIgnoreExtraElements]
    public class Items
    {
        [BsonId] //, BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public String ? Id { get; set; }

      
        public string Itemname { get; set; }

        
        public string Discription { get; set; }

     
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
