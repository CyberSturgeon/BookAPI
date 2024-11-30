using AutoMapper;
using Books.BLL.Models;
using Books.DAL.DTOs;

namespace Books.BLL.Mappings;

public class UserMapperProfile:Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserModel>();
        CreateMap<User, UserFullModel>()
            .ForMember(ufm => ufm.Books, opt =>opt.MapFrom(u => u.Books))
            .ForMember(ufm => ufm.Trades, opt => opt.MapFrom(u => u.Trades));
        CreateMap<CreateUserModel, User>();
        CreateMap<UpdateUserModel, User>();

    }
}
