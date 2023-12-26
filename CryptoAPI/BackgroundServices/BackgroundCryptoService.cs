using AutoMapper;
using CryptoAPI.BackgroundServices.HubSignalR;
using CryptoAPI.Data.Repositories;
using CryptoAPI.Models.BitStamp;
using CryptoAPI.Models.Dto;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

public class BackgroundCryptoService : BackgroundService
{
    readonly ILogger<BackgroundCryptoService> _logger;
    private readonly ICryptoRepository _cryptoRepository;
    private readonly IMapper _mapper;
    private readonly IHubContext<CryptoHub, ICryptoHub> _hubContext;
    private const string urlBitStamp = "wss://ws.bitstamp.net";
    public BackgroundCryptoService(IHubContext<CryptoHub, ICryptoHub> hubContext, IMapper mapper, ICryptoRepository cryptoRepository, ILogger<BackgroundCryptoService> logger)
    {
        _logger = logger;
        _cryptoRepository = cryptoRepository;
        _mapper = mapper;
        _hubContext = hubContext;
    }
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("CryptoService Started");
        var uri = new Uri($"{urlBitStamp}");
        using var clientWebSocket = new ClientWebSocket();

        try
        {
            await clientWebSocket.ConnectAsync(uri, stoppingToken);
            if (clientWebSocket.State == WebSocketState.Open)
            {
                await Subscribe(clientWebSocket, stoppingToken);
                var receiveBuffer = new ArraySegment<byte>(new byte[8192]);
                WebSocketReceiveResult result;
                do
                {
                    result = await clientWebSocket.ReceiveAsync(receiveBuffer, stoppingToken);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(receiveBuffer.Array, 0, result.Count);
                        LiveOrderBook orderBook = JsonConvert.DeserializeObject<LiveOrderBook>(message);
                        if (orderBook != null && orderBook._event == LiveOrderBook.Enums.eventSubscribeSuccess)
                        {
                            _logger.LogInformation("Received subscription response.");
                        }
                        else if (orderBook != null)
                        {
                            CreateOrderBookAndSendToClients(orderBook);
                            await Task.Delay(5000, stoppingToken);
                        }
                    }
                }
                while (!stoppingToken.IsCancellationRequested);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Error: {ex.Message}");
        }
    }

    private async void CreateOrderBookAndSendToClients(LiveOrderBook orderBook)
    {
        var orderBookDto = _mapper.Map<LiveOrderBookDto>(orderBook);
        _cryptoRepository.Create(orderBookDto);
        await _hubContext.Clients.All.BroadcastCrypto(orderBookDto);
    }

    private async Task Subscribe(ClientWebSocket clientWebSocket, CancellationToken stoppingToken)
    {
        List<string> pairs = new List<string> { LiveOrderBook.Enums.pairbtc, LiveOrderBook.Enums.paireth };

        foreach (var pair in pairs)
        {
            LiveOrderBook subscribeRequest = new LiveOrderBook();
            subscribeRequest._event = LiveOrderBook.Enums.eventSubscribe;
            subscribeRequest.data.channel = $"order_book_{pair}";
            var serializedRequest = JsonConvert.SerializeObject(subscribeRequest);
            var subscribeBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(serializedRequest));
            await clientWebSocket.SendAsync(subscribeBuffer, WebSocketMessageType.Text, true, stoppingToken);
        }
    }
}


