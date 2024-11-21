using Books.DAL.DTOs;

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
}
