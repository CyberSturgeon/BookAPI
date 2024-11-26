﻿using Books.DAL.DTOs;

namespace Books.DAL.Repositories.Interfaces;

public interface IUsersRepository
{
    public User? VerifyUser(string email, string password);

    public User? GetUserByEmail(string email);

    public User? GetUserById(Guid id);

    public ICollection<User>? GetUsers();

    public void DeleteUser(User user);

    public void UpdateUser(User user);

    public Guid AddUser(User user);
}
