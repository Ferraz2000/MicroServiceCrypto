using CryptoAPI.Data.Configurations;
using CryptoAPI.Models;
using CryptoAPI.Models.Dto;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace CryptoAPI.Data.Repositories
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly IMongoCollection<LiveOrderBookDto> _cryptoCollection;
        public CryptoRepository(IDataBaseConfig dataBaseConfig)
        {

            var mongoClient = new MongoClient(dataBaseConfig.ConnectionString);
            var database = mongoClient.GetDatabase(dataBaseConfig.DatabaseName);
            _cryptoCollection = database.GetCollection<LiveOrderBookDto>("cryptos");
        }
        public void Create(LiveOrderBookDto crypto)
        {
            _cryptoCollection.InsertOne(crypto);
        }

        public IEnumerable<LiveOrderBookDto> Get()
        {
            return _cryptoCollection.Find(tarefa => true).ToList();
        }

        public LiveOrderBookDto GetMostRecent(string moeda)
        {
            string channel = $"order_book_{moeda}usd";
            var filter = Builders<LiveOrderBookDto>.Filter.Eq(x => x.channel, channel);

            var sort = Builders<LiveOrderBookDto>.Sort.Descending(x => x.Id);

            // Realiza a busca no banco de dados
            var latestRecord = _cryptoCollection.Find(filter).Sort(sort).FirstOrDefault();

            return latestRecord;
        }
    }
}
