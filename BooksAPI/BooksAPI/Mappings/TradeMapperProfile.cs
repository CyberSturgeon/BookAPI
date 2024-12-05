using AutoMapper;
using Books.BLL.Models;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;

namespace BooksAPI.Mappings;

public class TradeMapperProfile: Profile
{
    public TradeMapperProfile()
    {
        CreateMap<TradeModel, TradeRequestResponse>();
        CreateMap<TradeRequestRequest, TradeModel>();
    }
}
