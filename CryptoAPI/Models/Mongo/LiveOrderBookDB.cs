using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CryptoAPI.Models.Mongo
{
    public class LiveOrderBookDB

    {

        [BsonId, BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("data")]
        public DataOrderBookDB data { get; set; }

        [BsonElement("channel")]
        public string channel { get; set; }

        [BsonElement("event")]
        public string _event { get; set; }
    }
}

