using MongoDB.Bson;

namespace CryptoAPI.Models.Dto
{
    public class MelhorPrecoResponse
    {
        public MelhorPrecoResponse()
        {

        }
        public MelhorPrecoResponse(Cotacao cotacao, string moeda)
        {
            ResultadoCalculo = cotacao.ResultadoCalculado;
            QuantidadeSolicitada = cotacao.QuantidadeSolicitada;
            IdCotacao = ObjectId.GenerateNewId();
            TipoOperacao = cotacao.TipoOperacao;
            ColecaoUtilizada = cotacao.ColecaoUtilizada;
            Moeda = moeda;
        }
        public ObjectId IdCotacao { get; set; }
        public List<decimal[]> ColecaoUtilizada { get; set; }
        public decimal QuantidadeSolicitada { get; set; }
        public decimal ResultadoCalculo { get; set; }
        public string TipoOperacao { get; set; }

        public string Moeda { get; set; }
    }
}
