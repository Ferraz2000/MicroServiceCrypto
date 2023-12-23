using MongoDB.Bson;

namespace CryptoAPI.Models
{
    public class MelhorPrecoResponse
    {
        public MelhorPrecoResponse()
        {

        }
        public MelhorPrecoResponse(Cotacao cotacao,string moeda)
        {
            this.ResultadoCalculo = cotacao.ResultadoCalculado;
            this.QuantidadeSolicitada = cotacao.QuantidadeSolicitada;
            this.IdCotacao = ObjectId.GenerateNewId();
            this.TipoOperacao = cotacao.TipoOperacao;
            this.ColecaoUtilizada = cotacao.ColecaoUtilizada;
            this.Moeda = moeda;
        }
        public ObjectId IdCotacao { get; set; }
        public List<decimal[]> ColecaoUtilizada { get; set; }
        public decimal QuantidadeSolicitada { get; set; }
        public decimal ResultadoCalculo { get; set; }
        public string TipoOperacao { get; set; }

        public string Moeda { get; set; }
    }
}
