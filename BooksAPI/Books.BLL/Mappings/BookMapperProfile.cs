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
            .ForMember(bfm => bfm.Users, opt => opt.MapFrom(b => b.Users))
            .ForMember(bfm => bfm.TradeRequests, opt => opt.MapFrom(b => b.TradeRequests));
        CreateMap<CreateBookModel, Book>();
        CreateMap<UpdateBookModel, Book>();
        CreateMap<BookFilterModel, BookFilter>();
    }
}
