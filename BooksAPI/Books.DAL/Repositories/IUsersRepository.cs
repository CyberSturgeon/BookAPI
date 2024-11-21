using Books.DAL.DTOs;

namespace Books.DAL.Repositories;

public interface IUsersRepository
{
    public User? VerifyUser(string email, string password);
}
