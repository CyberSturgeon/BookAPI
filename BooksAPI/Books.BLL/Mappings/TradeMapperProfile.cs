
using AutoMapper;
using Books.BLL.Models;
using Books.DAL.DTOs;

namespace Books.BLL.Mappings;

public class TradeMapperProfile: Profile
{
    public TradeMapperProfile()
    {
        CreateMap<TradeRequest, TradeModel>()
            .ForMember(t => t.BookOffer, opt => opt.MapFrom(tr => tr.BookOffer))
            .ForMember(t => t.Book, opt => opt.MapFrom(tr => tr.Book))
            .ForMember(t => t.Owner, opt => opt.MapFrom(tr => tr.Owner))
            .ForMember(t => t.Buyer, opt => opt.MapFrom(tr => tr.Buyer));
        CreateMap<TradeModel, TradeRequest>()
            .ForMember(tr => tr.BookOffer, opt => opt.MapFrom(t => t.BookOffer))
            .ForMember(tr => tr.Book, opt => opt.MapFrom(t => t.Book))
            .ForMember(tr => tr.Owner, opt => opt.MapFrom(t => t.Owner))
            .ForMember(tr => tr.Buyer, opt => opt.MapFrom(t => t.Buyer));
    }
}
