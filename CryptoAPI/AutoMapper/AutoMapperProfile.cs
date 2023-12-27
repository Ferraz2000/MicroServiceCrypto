using AutoMapper;
using CryptoAPI.Models.BitStamp;
using CryptoAPI.Models.Dto;
using CryptoAPI.Models.Mongo;
using MongoDB.Bson;

namespace CryptoAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<LiveOrderBook, LiveOrderBookDB>();
            CreateMap<LiveOrderBookDB, LiveOrderBook>();
            CreateMap<DataOrderBook, DataOrderBookDB>();
            CreateMap<DataOrderBookDB, DataOrderBook>();
            CreateMap<MelhorPrecoResponse, MelhorPrecoResponseDB>();
            CreateMap<MelhorPrecoResponseDB, MelhorPrecoResponse>();
        }
    }
}
