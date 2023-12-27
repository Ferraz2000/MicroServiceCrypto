using CryptoAPI.Data.Configurations;
using CryptoAPI.Models;
using CryptoAPI.Models.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace CryptoAPI.Data.Repositories
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly IMongoCollection<LiveOrderBookDB> _cryptoCollection;
        public CryptoRepository(IDataBaseConfig dataBaseConfig)
        {

            var mongoClient = new MongoClient(dataBaseConfig.ConnectionString);
            var database = mongoClient.GetDatabase(dataBaseConfig.DatabaseName);
            _cryptoCollection = database.GetCollection<LiveOrderBookDB>("cryptos");
        }
        public void Create(LiveOrderBookDB crypto)
        {
            _cryptoCollection.InsertOne(crypto);
        }

        public IEnumerable<LiveOrderBookDB> Get()
        {
            return _cryptoCollection.Find(tarefa => true).ToList();
        }

        public LiveOrderBookDB GetMostRecent(string moeda)
        {
            string channel = $"order_book_{moeda}usd";
            var filter = Builders<LiveOrderBookDB>.Filter.Eq(x => x.channel, channel);

            var sort = Builders<LiveOrderBookDB>.Sort.Descending(x => x.Id);

            // Realiza a busca no banco de dados
            var latestRecord = _cryptoCollection.Find(filter).Sort(sort).FirstOrDefault();

            return latestRecord;
        }
    }
}
