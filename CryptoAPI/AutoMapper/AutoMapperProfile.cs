using AutoMapper;
using CryptoAPI.Models;
using CryptoAPI.Models.BitStamp;
using CryptoAPI.Models.Dto;
using MongoDB.Bson;

namespace CryptoAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<LiveOrderBook, LiveOrderBookDto>();
            CreateMap<LiveOrderBookDto, LiveOrderBook>();
            CreateMap<DataOrderBook, DataOrderBookDto>();
            CreateMap<DataOrderBookDto, DataOrderBook>();
            CreateMap<MelhorPrecoResponseDTO, MelhorPrecoResponse>();
            CreateMap<MelhorPrecoResponse, MelhorPrecoResponseDTO>();
        }
    }
}
