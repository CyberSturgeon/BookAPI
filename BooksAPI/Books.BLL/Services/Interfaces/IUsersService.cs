using Books.DAL.DTOs;

namespace Books.BLL.Services.Interfaces;

public interface IUsersService
{
    public User VerifyUser(string email, string password);

    public string? LogIn(User user);
}
