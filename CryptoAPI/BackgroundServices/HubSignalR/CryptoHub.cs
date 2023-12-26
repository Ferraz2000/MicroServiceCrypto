using CryptoAPI.Models.Dto;
using Microsoft.AspNetCore.SignalR;
namespace CryptoAPI.BackgroundServices.HubSignalR
{
    public class CryptoHub : Hub<ICryptoHub>
    {
    }
}
