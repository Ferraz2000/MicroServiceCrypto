using CryptoAPI.Data.Configurations;
using CryptoAPI.Models.Mongo;
using MongoDB.Driver;

namespace CryptoAPI.Data.Repositories
{
    public class MelhorPrecoRepository : IMelhorPrecoRepository
    {
        private readonly IMongoCollection<MelhorPrecoResponseDB> _melhorPrecoCollection;
        public MelhorPrecoRepository(IDataBaseConfig dataBaseConfig)
        {
            var mongoClient = new MongoClient(dataBaseConfig.ConnectionString);
            var database = mongoClient.GetDatabase(dataBaseConfig.DatabaseName);
            _melhorPrecoCollection = database.GetCollection<MelhorPrecoResponseDB>("MelhorPreco");
        }
        public void Create(MelhorPrecoResponseDB melhorPreco)
        {
            _melhorPrecoCollection.InsertOne(melhorPreco);
        }

    }
}
