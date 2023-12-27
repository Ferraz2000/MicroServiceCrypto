using CryptoAPI.Models;
using CryptoAPI.Models.Mongo;

namespace CryptoAPI.Data.Repositories
{
    public interface ICryptoRepository
    {
        public IEnumerable<LiveOrderBookDB> Get();
        public LiveOrderBookDB GetMostRecent(string moeda);
        public void Create(LiveOrderBookDB crypto);
    }
}
