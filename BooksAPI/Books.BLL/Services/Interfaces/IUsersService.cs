using Books.BLL.Models;

namespace Books.BLL.Services.Interfaces
{
    public interface IUsersService
    {
        void AddUser(UserModel user);
        void DeleteUser(Guid id);
        ICollection<UserModel> GetAllUsers();
        UserModel GetUserByEmail(string email);
        UserFullModel GetUserById(Guid id);
        void UpdateUser(Guid id, UpdateUserModel newUser);
        string VerifyUser(string email, string password);
    }
}