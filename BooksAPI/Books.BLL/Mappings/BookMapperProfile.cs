using AutoMapper;
using Books.BLL.Models;
using Books.DAL.DTOs;

namespace Books.BLL.Mappings;

public class BookMapperProfile : Profile
{
    public BookMapperProfile() 
    {
        CreateMap<Book, BookModel>();
        CreateMap<Book, BookFullModel>()
            .ForMember(b => b.Users, opt => opt.Ignore())
            .ForMember(b => b.TradeRequests, opt => opt.Ignore());
        CreateMap<CreateBookModel, Book>();
        CreateMap<UpdateBookModel, Book>()
            .ForMember(b => b.Users, opt => opt.Ignore())
            .ForMember(b => b.TradeRequests, opt => opt.Ignore());

    }
}
