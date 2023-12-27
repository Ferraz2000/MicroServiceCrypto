using CryptoAPI.Models.Mongo;

namespace CryptoAPI.Data.Repositories
{
    public interface IMelhorPrecoRepository
    {
        public void Create(MelhorPrecoResponseDB melhorPreco);

    }
}
