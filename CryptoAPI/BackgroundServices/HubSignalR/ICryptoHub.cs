using CryptoAPI.Models.Dto;

namespace CryptoAPI.BackgroundServices.HubSignalR
{
    public interface ICryptoHub
    {
        public Task BroadcastCrypto( LiveOrderBookDto orderbook);
    }
}
