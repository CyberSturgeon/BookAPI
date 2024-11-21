using Books.DAL.DTOs;

namespace Books.DAL.Repositories.Interfaces;

public interface IUsersRepository
{
    public User? VerifyUser(string email, string password);

    public User? GetUserByEmail(string email);
}
