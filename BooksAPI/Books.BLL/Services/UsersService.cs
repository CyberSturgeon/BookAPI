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
    private IUsersRepository _usersRepository;

    private IBooksRepository _booksRepository;

    private readonly Mapper _mapper;

    public UsersService()
    {
        _usersRepository = new UsersRepository();

        _booksRepository = new BooksRepository();

        var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new UserMapperProfile());
                });
        _mapper = new Mapper(config);
    }

    public string VerifyUser(string email, string password)
    {
        return LogIn(_mapper.Map<UserModel>(_usersRepository.VerifyUser(email, password))) ??
            throw new WrongLoginException($"Wrong Email or password, try again");
    }

    public UserModel GetUserByEmail(string email)
    {
        return _mapper.Map<UserModel>(_usersRepository.GetUserByEmail(email)) ??
            throw new EntityNotFoundException($"User {email} not found");
    }

    public UserFullModel GetUserById(Guid id)
    {
        return _mapper.Map<UserFullModel>(_usersRepository.GetUserFullProfileById(id)) ??
            throw new EntityNotFoundException($"User {id} not found");
    }

    public ICollection<UserModel> GetAllUsers()
    {
        return _mapper.Map<List<UserModel>>(_usersRepository.GetUsers()) ??
            throw new EntityNotFoundException($"Users not found");
    }

    public void DeleteUser(Guid id)
    {
        var user = _usersRepository.GetUserById(id) ??
            throw new EntityNotFoundException($"User {id} not found");

        _usersRepository.DeleteUser(user);
    }

    public void UpdateUser(Guid id, UpdateUserModel newUserModel)
    {
        var user = _usersRepository.GetUserById(id) ??
            throw new EntityNotFoundException($"User {id} not found");

        var newUser = _mapper.Map<User>(newUserModel) ??
            throw new EntityInvalidDataException($"Data to update user {id} is invalid");

        _usersRepository.UpdateUser(user, newUser);
    }

    public Guid AddUser(CreateUserModel userModel)
    {
        var existsUser = _usersRepository.GetUserByEmail(userModel.Email);

        if (existsUser != null)
        {
            throw new EntityConflictException($"User {userModel.Email} already exists");
        }

        var userDto = _mapper.Map<User>(userModel) ??
            throw new EntityInvalidDataException($"Data to create user {userModel.Email} is invalid");
        //userDto.Password = HashCode it

        var id = _usersRepository.AddUser(userDto);
        return id;
    }

    public void AddBookToUser(Guid userId, Guid bookId)
    {
        var user = _usersRepository.GetUserFullProfileById(userId) ??
            throw new EntityNotFoundException($"User {userId} not found");

        var book = _booksRepository.GetBookById(bookId) ??
            throw new EntityNotFoundException($"Book {bookId} not found");

        _usersRepository.AddBookToUser(book, user);
    }

    private string? LogIn(UserModel userModel)
    {
        if (userModel != null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: Options.Issuer,
                audience: Options.Audience,
                claims: new List<Claim>() { new Claim("string", userModel.Id.ToString()), new Claim("string", userModel.Name) },
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
