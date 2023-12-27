using AutoMapper;
using CryptoAPI.BackgroundServices.HubSignalR;
using CryptoAPI.Data.Repositories;
using CryptoAPI.Models.BitStamp;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace CryptoAPI.BackgroundServices
{
    public class BackgroundCryptoService : BackgroundService
    {
        readonly ILogger<BackgroundCryptoService> _logger;
        private readonly ICryptoRepository _cryptoRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<CryptoHub, ICryptoHub> _hubContext;
        private readonly string urlBitStamp;
        public BackgroundCryptoService(IConfiguration configuration, IHubContext<CryptoHub, ICryptoHub> hubContext, IMapper mapper, ICryptoRepository cryptoRepository, ILogger<BackgroundCryptoService> logger)
        {
            _logger = logger;
            _cryptoRepository = cryptoRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            urlBitStamp = configuration["urlbitstamp"];
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
                            CryptoAPI.Models.BitStamp.LiveOrderBook orderBook = JsonConvert.DeserializeObject<CryptoAPI.Models.BitStamp.LiveOrderBook>(message);
                            if (orderBook != null && orderBook._event == CryptoAPI.Models.BitStamp.LiveOrderBook.Enums.eventSubscribeSuccess)
                            {
                                _logger.LogInformation("Received subscription response.");
                            }
                            else if (orderBook != null)
                            {
                                await CreateOrderBookAndSendToClients(orderBook);
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

        private async Task CreateOrderBookAndSendToClients(CryptoAPI.Models.BitStamp.LiveOrderBook orderBook)
        {
            var orderBookdb = _mapper.Map<CryptoAPI.Models.Mongo.LiveOrderBookDB>(orderBook);
            _cryptoRepository.Create(orderBookdb);
            await _hubContext.Clients.All.BroadcastCrypto(orderBookdb);
        }

        private async Task Subscribe(ClientWebSocket clientWebSocket, CancellationToken stoppingToken)
        {
            List<string> pairs = new List<string> { CryptoAPI.Models.BitStamp.LiveOrderBook.Enums.pairbtc, CryptoAPI.Models.BitStamp.LiveOrderBook.Enums.paireth };

            foreach (var pair in pairs)
            {
                CryptoAPI.Models.BitStamp.LiveOrderBook subscribeRequest = new CryptoAPI.Models.BitStamp.LiveOrderBook();
                subscribeRequest._event = CryptoAPI.Models.BitStamp.LiveOrderBook.Enums.eventSubscribe;
                subscribeRequest.data.channel = $"order_book_{pair}";
                var serializedRequest = JsonConvert.SerializeObject(subscribeRequest);
                var subscribeBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(serializedRequest));
                await clientWebSocket.SendAsync(subscribeBuffer, WebSocketMessageType.Text, true, stoppingToken);
            }
        }
    }
}