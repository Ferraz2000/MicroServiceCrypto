using Newtonsoft.Json;

namespace CryptoWeb.Models
{
    public class DataOrderBook
    {
        [JsonProperty("timestamp")]
        public string timestamp { get; set; }

        [JsonProperty("microtimestamp")]
        public string microtimestamp { get; set; }


        [JsonProperty("bids")]
        public List<string[]> Bids { get; set; }

        [JsonProperty("asks")]
        public List<string[]> Asks { get; set; }

        [JsonProperty("channel")]
        public string channel { get; set; }
    }
}
