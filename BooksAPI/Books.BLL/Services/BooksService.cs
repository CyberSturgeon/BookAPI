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
<<<<<<< HEAD
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
                    cfg.AddProfile(new TradeMapperProfile());
                });
        _mapper = new Mapper(config);
    }

=======
>>>>>>> di-experiments
    public Guid AddBook(CreateBookModel bookModel)
    {
        var owner = usersRepository.GetUserById(bookModel.UserId) ??
            throw new EntityNotFoundException($"User {bookModel.UserId} not found");

        var book = mapper.Map<Book>(bookModel);

        var id = booksRepository.AddBook(book, owner);

        booksRepository.AddUserToBook(book, owner);

        return id;
    }

    public List<BookModel> GetBooksByFilter(BookFilterModel filter)
    {
        return _mapper.Map<List<BookModel>>(_booksRepository.GetBooksByFilter(_mapper.Map<BookFilter>(filter)));
    }

    public BookFullModel GetBookById(Guid id)
    {
<<<<<<< HEAD
        return _mapper.Map<BookFullModel>(_booksRepository.GetBookFullProfileById(id)) ??
            throw new EntityNotFoundException($"Book {id} not found"); 
=======
        return mapper.Map<BookFullModel>(booksRepository.GetBookFullProfileById(id)) ??
            throw new EntityNotFoundException($"Book {id} not found"); ;
>>>>>>> di-experiments
    }

    public ICollection<BookModel> GetAllBooks()
    {
<<<<<<< HEAD
        return _mapper.Map<List<BookModel>>(_booksRepository.GetBooks());
=======
        return mapper.Map<List<BookModel>>(booksRepository.GetBooks()) ??
            throw new EntityNotFoundException("Books not found");
>>>>>>> di-experiments
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

        var newBook = mapper.Map<Book>(newBookModel) ??
            throw new EntityNotFoundException($"Data to update book {id} is invalid");

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
