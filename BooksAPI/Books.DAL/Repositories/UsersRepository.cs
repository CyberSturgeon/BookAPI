using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.DAL.Repositories;

public class UsersRepository(BooksContext context) : IUsersRepository
{
    public User? VerifyUser(string email, string password) => context.Users
            .FirstOrDefault(u => u.Email == email && u.Password == password);

    public User? GetUserByEmail(string email) => context.Users
            .FirstOrDefault(u => u.Email == email);

    public User? GetUserById(Guid id) => context.Users
            .FirstOrDefault(u => u.Id == id);

    public User? GetUserFullProfileById(Guid id) => context.Users
            .Include(u => u.Books)
            .Include(u => u.Trades)
            .FirstOrDefault(u => u.Id == id);

    public ICollection<User>? GetUsers() => context.Users.ToList();

    public void DeleteUser(User user)
    {
        context.Users.Remove(user);
        context.SaveChanges();
    }

    public void UpdateUser(User user, User newUser)
    {
        user.Name = newUser.Name;
        user.Email = newUser.Email;
        user.Trades = newUser.Trades;
        user.Books = newUser.Books;
        
        context.SaveChanges();
    }

    public void AddBookToUser(Book book, User owner)
    {
        owner.Books.Add(book);

        context.SaveChanges();
    }

    public void UpdateUserPassword(User user, string password)
    {
        user.Password = password;

        context.SaveChanges();
    }

    public Guid AddUser(User user)
    {
        user.Trades = new List<TradeRequest>();
        user.Books = new List<Book>();

        context.Users.Add(user);
        context.SaveChanges();

        return user.Id;
    }
}
