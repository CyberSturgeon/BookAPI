using Books.DAL.DTOs;
namespace Books.DAL.Repositories.Interfaces;

public class BooksRepository : IBooksRepository
{
    private BooksContext _context;
    
    private readonly IUsersRepository _usersRepository;

    public BooksRepository()
    {
        _usersRepository = new UsersRepository();

        _context = new BooksContext();
    }

    public Book? GetBookById(Guid id)
    {
        return _context.Books.Where(b => b.Id == id).FirstOrDefault();
    }

    public ICollection<Book>? GetBooks()
    {
        return _context.Books.ToList();
    }

    public ICollection<Book>? GetBooksByFilter(BooksFilter filter)
    {

        return _context.Books.Where(b => b.Name == filter.Name && b.Author == filter.Author && b.Genre == filter.Genre).ToList();
    }

    public void DeleteBook(Book book)
    {
        _context.Books.Remove(book);
        _context.SaveChanges();
    }

    public void UpdateBook(Book book, Book newBook)
    {
        book.Name = newBook.Name;
        book.Author = newBook.Author;
        book.Genre = newBook.Genre;

        _context.SaveChanges();
    }

    public Guid AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();

        return book.Id;
    }

    public void AddUserToBook(Book book, User owner)
    {
        book.Users.Add(owner);

        _context.SaveChanges();
    }

    public void AddTradeToBook(Book book, TradeRequest trade)
    {
        book.TradeRequests.Add(trade);

        _context.SaveChanges();
    }
}
