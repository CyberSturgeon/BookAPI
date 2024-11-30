using AutoMapper;
using Books.DAL.Repositories.Interfaces;
using Books.DAL.DTOs;
using Books.DAL.Repositories;
using Books.BLL.Mappings;
using Books.BLL.Models;
using Books.BLL.Exceptions;
using Books.BLL.Services.Interfaces;

namespace Books.BLL.Services;

public class BooksService : IBooksService
{
    private IBooksRepository _booksRepository;

    private IUsersRepository _usersRepository;

    private readonly Mapper _mapper;

    public BooksService()
    {
        _booksRepository = new BooksRepository();

        _usersRepository = new UsersRepository();

        var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new UserMapperProfile());
                    cfg.AddProfile(new BookMapperProfile());
                });
        _mapper = new Mapper(config);
    }

    public Guid AddBook(CreateBookModel bookModel)
    {
        var owner = _usersRepository.GetUserById(bookModel.UserId) ??
            throw new EntityNotFoundException($"User {bookModel.UserId} not found");

        var book = _mapper.Map<Book>(bookModel);

        var id = _booksRepository.AddBook(book, owner);

        _booksRepository.AddUserToBook(book, owner);

        return id;
    }

    public BookFullModel GetBookById(Guid id)
    {
        return _mapper.Map<BookFullModel>(_booksRepository.GetBookFullProfileById(id)) ??
            throw new EntityNotFoundException($"Book {id} not found"); ;
    }

    public ICollection<BookModel> GetAllBooks()
    {
        return _mapper.Map<List<BookModel>>(_booksRepository.GetBooks()) ??
            throw new EntityNotFoundException("Books not found");
    }

    public void DeleteBook(Guid id)
    {
        var book = _booksRepository.GetBookById(id) ??
            throw new EntityNotFoundException($"Book {id} not found");

        _booksRepository.DeleteBook(book);
    }

    public void UpdateBook(Guid id, UpdateBookModel newBookModel)
    {
        var book = _booksRepository.GetBookById(id) ??
            throw new EntityNotFoundException($"Book {id} not found");

        var newBook = _mapper.Map<Book>(newBookModel) ??
            throw new EntityNotFoundException($"Data to update book {id} is invalid");

        _booksRepository.UpdateBook(book, newBook);
    }

    public void AddUserToBook(Guid bookId, Guid userId)
    {
        var book = _booksRepository.GetBookFullProfileById(bookId) ??
            throw new EntityNotFoundException($"Book {bookId} not found");

        var user = _usersRepository.GetUserById(userId) ??
            throw new EntityNotFoundException($"User {userId} not found");

        _booksRepository.AddUserToBook(book, user);
    }
}
