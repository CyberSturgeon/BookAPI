using AutoMapper;
using Books.BLL.Exceptions;
using Books.BLL.Mappings;
using Books.BLL.Models;
using Books.BLL.Services.Interfaces;
using Books.Core;
using Books.DAL.DTOs;
using Books.DAL.Repositories;
using Books.DAL.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

    public string VerifyUser(string email, string password)
    {
        return LogIn(_mapper.Map<UserModel>(_repository.VerifyUser(email, password))) ??
            throw new WrongLoginException($"Wrong Email or password, try again");
    }

    private string? LogIn(UserModel user)
    {
        if (user != null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: Options.Issuer,
                audience: Options.Audience,
                claims: new List<Claim>() { new Claim("string", user.Id.ToString()), new Claim("string", user.Name) },
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

    public UserModel GetUserByEmail(string email)
    {
        return _mapper.Map<UserModel>(_repository.GetUserByEmail(email)) ??
            throw new EntityNotFoundException($"User {email} not found");
    }

    public UserFullModel GetUserById(Guid id)
    {
        return _mapper.Map<UserFullModel>(_repository.GetUserFullProfileById(id)) ??
            throw new EntityNotFoundException($"User {id} not found");
    }

    public ICollection<UserModel> GetAllUsers()
    {
        return _mapper.Map<List<UserModel>>(_repository.GetUsers()) ??
            throw new EntityNotFoundException($"Users not found");
    }

    public void DeleteUser(Guid id)
    {
        var user = _repository.GetUserById(id) ??
            throw new EntityNotFoundException($"User {id} not found");

        _repository.DeleteUser(user);
    }

    public void UpdateUser(Guid id, UpdateUserModel newUser)
    {
        var user = _repository.GetUserById(id) ??
            throw new EntityNotFoundException($"User {id} not found");

        var newUserDTO = _mapper.Map<User>(user) ??
            throw new EntityInvalidDataException($"Data to update user {id} is invalid");

        _repository.UpdateUser(user, newUserDTO);
    }

    public void AddUser(UserModel user)
    {
        var existsUser = GetUserByEmail(user.Email);

        if (existsUser != null)
        {
            throw new EntityConflictException($"User {user.Email} already exists");
        }

        var userDto = _mapper.Map<User>(user) ??
            throw new EntityInvalidDataException($"Data to create user {user.Email} is invalid");
        //userDto.Password = HashCode it

        _repository.AddUser(userDto);
    }
}
