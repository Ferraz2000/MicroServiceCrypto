
using CryptoAPI.Models.Dto;
using System.Globalization;

namespace CryptoAPI.Models
{
    public class Cotacao
    {
        public decimal QuantidadeSolicitada { get; private set; }
        public string TipoOperacao { get; private set; }
        public decimal ResultadoCalculado { get; private set; }

        public List<decimal[]> ColecaoUtilizada { get; set; } = new List<decimal[]>();

        public Cotacao(LiveOrderBookDto _orderBook, decimal quantidadeSolicitada, string tipoOperacao)
        {
            QuantidadeSolicitada = quantidadeSolicitada;
            TipoOperacao = tipoOperacao;

            CalcularResultado(_orderBook);
        }

        private void CalcularResultado(LiveOrderBookDto orderBook)
        {
            List<decimal[]> compras = ConverterListaArrayStringParaListaDecimal(orderBook.data.Asks);
            List<decimal[]> vendas = ConverterListaArrayStringParaListaDecimal(orderBook.data.Bids);


            if (TipoOperacao == "compra")
            {
                var colecaoOrdenada = compras.OrderBy(item => item[0]).ToList();
                CalcularValorTotal(colecaoOrdenada);
            }
            else
            {
                var colecaoOrdenada = vendas.OrderByDescending(item => item[0]).ToList();
                CalcularValorTotal(colecaoOrdenada);
            }
        }

        private void CalcularValorTotal(List<decimal[]> colecaoOrdenada)
        {
            List<ItemCotacao> itensCotacao = CriarListaCotacaoPelaListaDecimal(colecaoOrdenada);
            decimal quantidadeTotal = 0;
            decimal valorTotal = 0;

            foreach (var item in itensCotacao)
            {
                if (quantidadeTotal < QuantidadeSolicitada)
                {
                    decimal quantidadeRestante = QuantidadeSolicitada - quantidadeTotal;

                    if (item.Quantidade >= quantidadeRestante)
                    {
                        quantidadeTotal += quantidadeRestante;
                        valorTotal += quantidadeRestante * item.Preco;
                        ColecaoUtilizada.Add(new decimal[] { item.Preco, quantidadeRestante });
                        break;
                    }
                    else
                    {
                        quantidadeTotal += item.Quantidade;
                        valorTotal += item.Quantidade * item.Preco;
                        ColecaoUtilizada.Add(new decimal[] { item.Preco, item.Quantidade, });
                    }
                }
            }

            ResultadoCalculado = valorTotal;
        }

        private List<ItemCotacao> CriarListaCotacaoPelaListaDecimal(List<decimal[]> listaDecimal)
        {
            List<ItemCotacao> items = new List<ItemCotacao>();

            foreach (var itemDecimal in listaDecimal)
            {
                ItemCotacao itemCotacao = new ItemCotacao(itemDecimal[1], itemDecimal[0]);
                items.Add(itemCotacao);
            }
            return items;
        }
        private List<decimal[]> ConverterListaArrayStringParaListaDecimal(List<string[]> listaArraysStrings)
        {
            var listaValores = new List<decimal[]>();

            foreach (var arrayStrings in listaArraysStrings)
            {
                decimal valorUSD = 0;
                decimal valorBTC = 0;

                bool parsedUSD = arrayStrings.Length > 0 && decimal.TryParse(arrayStrings[0], NumberStyles.Number, CultureInfo.InvariantCulture, out valorUSD);
                bool parsedBTC = arrayStrings.Length > 1 && decimal.TryParse(arrayStrings[1], NumberStyles.Number, CultureInfo.InvariantCulture, out valorBTC);

                if (parsedUSD && parsedBTC)
                {
                    listaValores.Add(new[] { valorUSD, valorBTC });
                }
            }
            return listaValores;
        }
    }

    public class ItemCotacao
    {
        public decimal Quantidade { get; private set; }
        public decimal Preco { get; private set; }

        public ItemCotacao(decimal quantidade, decimal preco)
        {
            Quantidade = quantidade;
            Preco = preco;
        }
    }

}
