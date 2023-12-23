using CryptoAPI.Models.Dto;
using Microsoft.AspNetCore.SignalR;
namespace CryptoAPI.BackgroundServices.HubSignalR
{
    public class CryptoHub : Hub
    {
        public void BroadcastCrypto(LiveOrderBookDto orderbook)
        {
            Clients.All.SendAsync("ReceiveOrderBook", orderbook);
        }
        public void BroadcastMessage(string message)
        {
            Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
