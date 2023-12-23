using CryptoAPI.Data.Configurations;
using CryptoAPI.Models.Dto;
using MongoDB.Driver;

namespace CryptoAPI.Data.Repositories
{
    public class MelhorPrecoRepository : IMelhorPrecoRepository
    {
        private readonly IMongoCollection<MelhorPrecoResponseDTO> _melhorPrecoCollection;
        public MelhorPrecoRepository(IDataBaseConfig dataBaseConfig)
        {
            var mongoClient = new MongoClient(dataBaseConfig.ConnectionString);
            var database = mongoClient.GetDatabase(dataBaseConfig.DatabaseName);
            _melhorPrecoCollection = database.GetCollection<MelhorPrecoResponseDTO>("MelhorPreco");
        }
        public void Create(MelhorPrecoResponseDTO melhorPreco)
        {
            _melhorPrecoCollection.InsertOne(melhorPreco);
        }

    }
}
