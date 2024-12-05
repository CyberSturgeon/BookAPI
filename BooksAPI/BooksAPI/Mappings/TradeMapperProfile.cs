using AutoMapper;
using Books.BLL.Models;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;

namespace BooksAPI.Mappings;

public class TradeMapperProfile: Profile
{
    public TradeMapperProfile()
    {
        CreateMap<TradeModel, TradeRequestResponse>()
            .ForMember(t => t.BookOffer, opt => opt.MapFrom(tm => tm.BookOffer))
            .ForMember(t => t.Book, opt => opt.MapFrom(tm => tm.Book))
            .ForMember(t => t.Owner, opt => opt.MapFrom(tm => tm.Owner))
            .ForMember(t => t.Buyer, opt => opt.MapFrom(tm => tm.Buyer));
        CreateMap<TradeRequestRequest, TradeRequestModel>();
    }
}
