using Newtonsoft.Json;

namespace CryptoWeb.Models
{
    public class LiveOrderBook
    {
        [JsonProperty("data")]
        public DataOrderBook data { get; set; }

        [JsonProperty("channel")]
        public string channel { get; set; }

        [JsonProperty("event")]
        public string _event { get; set; }

        public class Enums
        {
            
            public const string channelbtc = "order_book_btcusd";
            public const string channeleth = "order_book_ethusd";
            public const string pairbtc = "btcusd";
            public const string paireth = "ethusd";
            public const string eventData = "data";
            public const string eventSubscribe = "bts:subscribe";
            public const string eventSubscribeSuccess = "bts:subscription_succeeded";
        }
    }
}
