using Books.DAL.Repositories;
using Books.DAL.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Books.Core;
using Books.BLL.Services.Interfaces;
using Books.DAL.Repositories.Interfaces;

namespace Books.BLL.Servicies;

public class UsersService : IUsersService
{
    private IUsersRepository _repository;

    public UsersService()
    {
        _repository = new UsersRepository();
    }

    public User VerifyUser(string email, string password)
    {
        return _repository.VerifyUser(email, password);
    }

    public string? LogIn(User user)
    {
        if (user != null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: Options.Issuer,
                audience: Options.Audience,
                claims: new List<Claim>(),
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
}
