using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;

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
        return _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.Where(u => u.Email == email).FirstOrDefault();
    }

    public User? GetUserById(Guid id)
    {
        return _context.Users.Where(u => u.Id == id).FirstOrDefault();
    }

    public ICollection<User>? GetUsers()
    {
        return _context.Users.ToList();
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user, User newUser)
    {
        user.Name = newUser.Name;
        user.Email = newUser.Email;
        user.Trades = newUser.Trades;
        user.Books = newUser.Books;
        
        _context.SaveChanges();
    }

    public void AddBookToUser(Book book, User owner)
    {
        owner.Books.Add(book);

        _context.SaveChanges();
    }

    public void UpdateUserPassword(User user, string password)
    {
        user.Password = password;

        _context.SaveChanges();
    }

    public Guid AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return user.Id;
    }
}
