using Books.BLL.Models;

namespace Books.BLL.Services.Interfaces;

public interface IUsersService
{
    public UserModel VerifyUser(string email, string password);

    public string? LogIn(UserModel user);
}
