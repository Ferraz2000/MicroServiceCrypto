using AutoMapper;
using CryptoAPI.Data.Repositories;
using CryptoAPI.Models;
using CryptoAPI.Models.BitStamp;
using CryptoAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly IMelhorPrecoRepository _melhorPrecoRepository;
        private readonly IMapper _mapper;
        public CryptoController(ICryptoRepository cryptoRepository, IMelhorPrecoRepository melhorPrecoRepository, IMapper mapper)
        {
            _cryptoRepository = cryptoRepository;
            _melhorPrecoRepository = melhorPrecoRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult MelhorPreco([FromBody] MelhorPrecoRequest request)
        {
            LiveOrderBookDto mostRecentOrderBook = _cryptoRepository.GetMostRecent(request.Moeda);

            Cotacao cotacao = new Cotacao(mostRecentOrderBook, request.QuantidadeSolicitada, request.TipoOperacao);

            MelhorPrecoResponse response = new MelhorPrecoResponse(cotacao, request.Moeda);

            Create(response);

            return Ok(response);
        }

        private void Create(MelhorPrecoResponse melhorPreco)
        {
            var melhorPrecoDto = _mapper.Map<MelhorPrecoResponseDTO>(melhorPreco);
            _melhorPrecoRepository.Create(melhorPrecoDto);
        }

    }
}
