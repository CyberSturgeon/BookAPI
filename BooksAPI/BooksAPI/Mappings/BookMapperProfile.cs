using AutoMapper;
using Books.BLL.Models;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;

namespace BooksAPI.Mappings;

public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<CreateBookRequest, CreateBookModel>();
        CreateMap<UpdateBookRequest, UpdateBookModel>();
        CreateMap<BookModel, BookShortResponse>();
        CreateMap<BookFullModel, BookFullResponse>()
            .ForMember(bfr => bfr.TradeRequests, opt => opt.MapFrom(bfm => bfm.TradeRequests));
        CreateMap<SearchBookRequest, BookFilterModel>();
    }
}
