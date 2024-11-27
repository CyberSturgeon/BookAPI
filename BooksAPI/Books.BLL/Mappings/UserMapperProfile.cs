using AutoMapper;
using Books.BLL.Models;
using Books.DAL.DTOs;

namespace Books.BLL.Mappings;

public class UserMapperProfile:Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserModel>();
        CreateMap<User, UserFullModel>();
        CreateMap<User, UserFullModel>()
            .ForMember(u => u.Books, opt => opt.Ignore())
            .ForMember(u => u.Trades, opt => opt.Ignore());
        CreateMap<RegisterUserModel, User>();
        CreateMap<UpdateUserModel, User>();

    }
}
