using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.DAL.Repositories;

public class UsersRepository : IUsersRepository
{
    private BooksContext _context;

    public UsersRepository()
    {
        _context = new BooksContext();
    }

    public User? VerifyUser(string email, string password)
    {
        return _context.User.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
    }

    public User? GetUserByEmail(string email)
    {
        return _context.User.Where(u => u.Email == email).FirstOrDefault();
    }

    public User? GetUserById(Guid id)
    {
        return _context.User.Where(u => u.Id == id).FirstOrDefault();
    }

    public ICollection<User>? GetUsers(Guid id)
    {
        return _context.User.ToList();
    }

    public void DeleteUserById(User user)
    {
        _context.User.Remove(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        var userToUpdate = GetUserById(user.Id);

        userToUpdate.Name = user.Name;
        userToUpdate.Email = user.Email;
        userToUpdate.Password = user.Password;
        userToUpdate.Trades = user.Trades;
        userToUpdate.Books = user.Books;
        
        _context.SaveChanges();
    }

    public Guid AddUser(User user)
    {
        _context.User.Add(user);
        _context.SaveChanges();

        return user.Id;
    }
}
