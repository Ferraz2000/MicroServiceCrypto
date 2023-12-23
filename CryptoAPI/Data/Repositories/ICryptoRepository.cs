using CryptoAPI.Models;
using CryptoAPI.Models.Dto;

namespace CryptoAPI.Data.Repositories
{
    public interface ICryptoRepository
    {
        public IEnumerable<LiveOrderBookDto> Get();
        public LiveOrderBookDto GetMostRecent(string moeda);
        public void Create(LiveOrderBookDto crypto);
    }
}
