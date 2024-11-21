using Books.DAL.Repositories;
using Books.DAL.DTOs;

namespace Books.BLL.Servicies;

public class UsersService : IUsersService
{
    private IUsersRepository _repository;

    public UsersService()
    {
        _repository = new UsersRepository();
    }

    public User VerifyUser(string email, string password)
    {
        return _repository.VerifyUser(email, password);
    }
}
