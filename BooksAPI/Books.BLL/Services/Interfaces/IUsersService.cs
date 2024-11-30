using Books.BLL.Models;

namespace Books.BLL.Services.Interfaces
{
    public interface IUsersService
    {
        Guid AddUser(CreateUserModel user);
        void DeleteUser(Guid id);
        ICollection<UserModel> GetAllUsers();
        UserModel GetUserByEmail(string email);
        UserFullModel GetUserById(Guid id);
        void UpdateUser(Guid id, UpdateUserModel newUser);
        string VerifyUser(string email, string password);
    }
}