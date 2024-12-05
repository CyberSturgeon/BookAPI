

using AutoMapper;
using Books.BLL.Models;
using Books.DAL.DTOs;

namespace Books.BLL.Mappings;

public class TradeMapperProfile : Profile
{
    public TradeMapperProfile()
    {
        CreateMap<TradeRequest, TradeModel>();
        CreateMap<TradeModel, TradeRequest>();
    }
}
