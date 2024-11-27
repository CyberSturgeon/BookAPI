using Books.BLL.Models;

namespace Books.BLL.Services.Interfaces;

public interface IUsersService
{
    string VerifyUser(string email, string password);
}
