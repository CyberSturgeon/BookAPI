﻿using AutoMapper;
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

public class UsersService(IBooksRepository booksRepository,
        IUsersRepository usersRepository,
        IMapper mapper) : IUsersService
{
    public string VerifyUser(string email, string password)
    {
        return LogIn(mapper.Map<UserModel>(usersRepository.VerifyUser(email, password))) ??
            throw new WrongLoginException($"Wrong Email or password, try again");
    }

    public UserModel GetUserByEmail(string email)
    {
        return mapper.Map<UserModel>(usersRepository.GetUserByEmail(email)) ??
            throw new EntityNotFoundException($"User {email} not found");
    }

    public UserFullModel GetUserById(Guid id)
    {
        return mapper.Map<UserFullModel>(usersRepository.GetUserFullProfileById(id)) ??
            throw new EntityNotFoundException($"User {id} not found");
    }

    public ICollection<UserModel> GetAllUsers()
    {
        return mapper.Map<List<UserModel>>(usersRepository.GetUsers());
    }

    public void DeleteUser(Guid id)
    {
        var user = usersRepository.GetUserById(id) ??
            throw new EntityNotFoundException($"User {id} not found");

        usersRepository.DeleteUser(user);
    }

    public void UpdateUser(Guid id, UpdateUserModel newUserModel)
    {
        var user = usersRepository.GetUserById(id) ??
            throw new EntityNotFoundException($"User {id} not found");

        var newUser = mapper.Map<User>(newUserModel);

        usersRepository.UpdateUser(user, newUser);
    }

    public Guid AddUser(CreateUserModel userModel)
    {
        var existsUser = usersRepository.GetUserByEmail(userModel.Email);

        if (existsUser != null)
        {
            throw new EntityConflictException($"User {userModel.Email} already exists");
        }

        var userDto = mapper.Map<User>(userModel);
        //userDto.Password = HashCode it

        var id = usersRepository.AddUser(userDto);
        return id;
    }

    public void AddBookToUser(Guid userId, Guid bookId)
    {
        var user = usersRepository.GetUserFullProfileById(userId) ??
            throw new EntityNotFoundException($"User {userId} not found");

        var book = booksRepository.GetBookById(bookId) ??
            throw new EntityNotFoundException($"Book {bookId} not found");

        usersRepository.AddBookToUser(book, user);
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
