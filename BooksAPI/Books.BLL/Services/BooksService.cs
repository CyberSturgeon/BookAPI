using AutoMapper;
using Books.BLL.Exceptions;
using Books.BLL.Models;
using Books.BLL.Services.Interfaces;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;

namespace Books.BLL.Services;

public class BooksService(
        IBooksRepository booksRepository,
        IUsersRepository usersRepository,
        IMapper mapper
    ) : IBooksService
{
    public Guid AddBook(CreateBookModel bookModel)
    {
        var owner = usersRepository.GetUserById(bookModel.UserId) ??
            throw new EntityNotFoundException($"User {bookModel.UserId} not found");

        var book = mapper.Map<Book>(bookModel);

        var id = booksRepository.AddBook(book, owner);

        booksRepository.AddUserToBook(book, owner);

        return id;
    }

    public BookFullModel GetBookById(Guid id)
    {
        return mapper.Map<BookFullModel>(booksRepository.GetBookFullProfileById(id)) ??
            throw new EntityNotFoundException($"Book {id} not found"); ;
    }

    public ICollection<BookModel> GetAllBooks()
    {
        return mapper.Map<List<BookModel>>(booksRepository.GetBooks());
    }

    public ICollection<BookModel> GetBooksByFilter(BookFilterModel filter)
    {
        return mapper.Map<List<BookModel>>(booksRepository.GetBooksByFilter(mapper.Map<BookFilter>(filter)));
    }

    public void DeleteBook(Guid id)
    {
        var book = booksRepository.GetBookById(id) ??
            throw new EntityNotFoundException($"Book {id} not found");

        booksRepository.DeleteBook(book);
    }

    public void UpdateBook(Guid id, UpdateBookModel newBookModel)
    {
        var book = booksRepository.GetBookById(id) ??
            throw new EntityNotFoundException($"Book {id} not found");

        var newBook = mapper.Map<Book>(newBookModel);

        booksRepository.UpdateBook(book, newBook);
    }

    public void AddUserToBook(Guid bookId, Guid userId)
    {
        var book = booksRepository.GetBookFullProfileById(bookId) ??
            throw new EntityNotFoundException($"Book {bookId} not found");

        var user = usersRepository.GetUserById(userId) ??
            throw new EntityNotFoundException($"User {userId} not found");

        booksRepository.AddUserToBook(book, user);
    }
}
