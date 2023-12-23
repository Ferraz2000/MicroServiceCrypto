using Newtonsoft.Json;

namespace CryptoAPI.Models.BitStamp
{
    public class LiveOrderBook
    {
        [JsonProperty("data")]
        public DataOrderBook data { get; set; } = new DataOrderBook();

        [JsonProperty("channel")]
        public string channel { get; set; }

        [JsonProperty("event")]
        public string _event { get; set; }

        public class Enums
        {
            public const string pairbtc = "btcusd";
            public const string paireth = "ethusd";
            public const string eventData = "data";
            public const string eventSubscribe = "bts:subscribe";
            public const string eventSubscribeSuccess = "bts:subscription_succeeded";
        }
    }
}