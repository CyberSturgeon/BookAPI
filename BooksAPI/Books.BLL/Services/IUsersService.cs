using Books.DAL.DTOs;

namespace Books.BLL.Servicies;

public interface IUsersService
{
    public User VerifyUser(string email, string password);
}
