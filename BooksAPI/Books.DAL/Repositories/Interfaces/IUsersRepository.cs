using Books.DAL.DTOs;

namespace Books.DAL.Repositories.Interfaces;

public interface IUsersRepository
{
    User? VerifyUser(string email, string password);
    User? GetUserByEmail(string email);
    User? GetUserById(Guid id);
    ICollection<User>? GetUsers();
    void AddBookToUser(Book book, User owner);
    void DeleteUser(User user);
    void UpdateUser(User user, User newUser);
    Guid AddUser(User user);
    User? GetUserFullProfileById(Guid id);
    void RemoveBookFromUser(User user, Book book);

}
