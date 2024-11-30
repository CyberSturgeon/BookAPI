using AutoMapper;
using Books.BLL.Models;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;

namespace BooksAPI.Mappings;

public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<CreateBookRequest, CreateBookRequest>();
        CreateMap<UpdateBookRequest, UpdateBookModel>();
        CreateMap<BookModel, BookShortResponse>();
        CreateMap<BookFullModel, BookFullResponse>();
        CreateMap<SearchBookRequest, BookFilterModel>();
    }
}
