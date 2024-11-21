using Books.DAL.Repositories;
using Books.DAL.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Books.Core;
using Books.BLL.Services.Interfaces;
using Books.DAL.Repositories.Interfaces;
using Books.BLL.Models;
using AutoMapper;
using Books.BLL.Mappings;

namespace Books.BLL.Services;

public class UsersService : IUsersService
{
    private IUsersRepository _repository;

    private readonly Mapper _mapper;

    public UsersService()
    {
        _repository = new UsersRepository();

        var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new UserMapperProfile());
                });
        _mapper = new Mapper(config);
    }

    public UserModel? VerifyUser(string email, string password)
    {
        return _mapper.Map<UserModel>(_repository.VerifyUser(email, password));
    }

    public string? LogIn(UserModel user)
    {
        if (user != null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: Options.Issuer,
                audience: Options.Audience,
                claims: new List<Claim>(){ new Claim("string", user.Id.ToString()), new Claim("string", user.Name) },
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        else
        {
            return null;
        }
    }

    public UserModel? GetUserByEmail(string email)
    {
        return _mapper.Map<UserModel>(_repository.GetUserByEmail(email));
    }
}
