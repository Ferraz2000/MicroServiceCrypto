using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CryptoAPI.Models.Dto
{
    public class MelhorPrecoResponseDTO
    {
        [BsonId, BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public string IdCotacao { get; set; }

        [BsonElement("colecaoUtilizada")]
        public List<decimal[]> ColecaoUtilizada { get; set; }

        [BsonElement("quantidadeSolicitada")]
        public decimal QuantidadeSolicitada { get; set; }

        [BsonElement("resultadoCalculo")]
        public decimal ResultadoCalculo { get; set; }

        [BsonElement("tipoOperacao")]
        public string TipoOperacao { get; set; }


        [BsonElement("moeda")]
        public string Moeda { get; set; }

    }
}




