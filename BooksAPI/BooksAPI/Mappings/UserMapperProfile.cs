using AutoMapper;
using Books.BLL.Models;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;

namespace BooksAPI.Mappings;

public class UserMapperProfile:Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterUserRequest, CreateUserModel>();
        CreateMap<UpdateUserRequest, UpdateUserModel>();    
        CreateMap<UserModel, UserResponse>();
        CreateMap<UserFullModel, UserFullResponse>()
            .ForMember(ufr => ufr.TradeRequests, opt => opt.MapFrom(ufm => ufm.Trades));   
    }
}
