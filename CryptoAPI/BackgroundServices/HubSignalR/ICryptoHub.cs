using CryptoAPI.Models.Mongo;

namespace CryptoAPI.BackgroundServices.HubSignalR
{
    public interface ICryptoHub
    {
        public Task BroadcastCrypto(LiveOrderBookDB orderbook);
    }
}
