namespace CryptoWeb.Models
{
    public class ItemTabelaCryptoHub
    {
        public ItemTabelaCryptoHub(List<decimal[]> precosusdcrypto, List<decimal[]> listaValoresAcumulados)
        {
            MaiorPreco = EncontrarMaiorPrecoUSD(precosusdcrypto);
            MenorPreco = EncontrarMenorPrecoUSD(precosusdcrypto);
            Media = CalcularMediaPreco(precosusdcrypto);
            MediaPrecoAcumulada = CalcularMediaPrecoAcumulado(precosusdcrypto, listaValoresAcumulados);
            MediaAcumulada = CalcularMediaCryptoAcumulada(precosusdcrypto);
        }
        public decimal[] MaiorPreco { get; set; }
        public decimal[] MenorPreco { get; set; }
        public decimal[] Media { get; set; }
        public decimal[] MediaPrecoAcumulada { get; set; }
        public decimal MediaAcumulada { get; set; }

        public static decimal[] EncontrarMenorPrecoUSD(List<decimal[]> listaValores)
        {
            if (listaValores == null || listaValores.Count == 0)
            {
                throw new ArgumentException("A lista de valores está vazia ou é nula.");
            }

            decimal[] menorPreco = listaValores.OrderBy(arr => arr[0]).First();

            return menorPreco;
        }
        public static decimal[] EncontrarMaiorPrecoUSD(List<decimal[]> listaValores)
        {
            if (listaValores == null || listaValores.Count == 0)
            {
                throw new ArgumentException("A lista de valores está vazia ou é nula.");
            }

            decimal[] maiorPreco = listaValores.OrderByDescending(arr => arr[0]).First();

            return maiorPreco;
        }
        public static decimal[] CalcularMediaPreco(List<decimal[]> listaValores)
        {

            if (listaValores == null || listaValores.Count == 0)
            {
                throw new ArgumentException("A lista de valores está vazia ou é nula.");
            }

            decimal mediaPrecoUSD = listaValores.Average(arr => arr[0]); // Calcula a média dos valores de USD (posição 0)

            var valorMaisProximo = listaValores
                .OrderBy(arr => Math.Abs(arr[0] - mediaPrecoUSD))
                .First();

            return valorMaisProximo;
        }
        public static decimal[] CalcularMediaPrecoAcumulado(List<decimal[]> listaValores, List<decimal[]> listaValoresAcumulados)
        {
            if (listaValoresAcumulados != null)
                listaValores.AddRange(listaValoresAcumulados);
            if (listaValores == null || listaValores.Count == 0)
            {
                throw new ArgumentException("A lista de valores está vazia ou é nula.");
            }

            decimal mediaPrecoUSD = listaValores.Average(arr => arr[0]); // Calcula a média dos valores de USD (posição 0)

            var valorMaisProximo = listaValores
                .OrderBy(arr => Math.Abs(arr[0] - mediaPrecoUSD))
                .First();

            return valorMaisProximo;
        }

        public static decimal CalcularMediaCryptoAcumulada(List<decimal[]> listaValores)
        {
            if (listaValores == null || listaValores.Count == 0)
            {
                throw new ArgumentException("A lista de valores está vazia ou é nula.");
            }
            decimal mediaPrecoCrypto = listaValores.Average(arr => arr[1]); // Calcula a média dos valores de Crypto (posição 1)

            var valorMaisProximo = listaValores
               .OrderBy(arr => Math.Abs(arr[0] - mediaPrecoCrypto))
               .First();

            return valorMaisProximo[1];
        }


    }
}
