using MongoDB.Bson.Serialization.Attributes;

namespace CryptoAPI.Models.Dto
{
    public class DataOrderBookDto
    {

        [BsonElement("timestamp")]
        public string timestamp { get; set; }

        [BsonElement("microtimestamp")]
        public string microtimestamp { get; set; }

        [BsonElement("bids")]
        public List<string[]> Bids { get; set; }

        [BsonElement("asks")]
        public List<string[]> Asks { get; set; }

        [BsonElement("channel")]
        public string channel { get; set; }
    }
}
