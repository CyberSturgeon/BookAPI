
using Books.DAL.DTOs;

namespace Books.DAL.Repositories.Interfaces;

public class BooksRepository : IBooksRepository
{
    private BooksContext _context;

    public BooksRepository()
    {
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

    public void DeleteBook(Book book)
    {
        _context.Books.Remove(book);
        _context.SaveChanges();
    }

    public void UpdateBook(Book book)
    {
        var bookToUpdate = GetBookById(book.Id);

        bookToUpdate.Name = book.Name;
        bookToUpdate.Author = book.Author;
        bookToUpdate.Genre = book.Genre;

        _context.SaveChanges();
    }

    public Guid AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();

        return book.Id;
    }

    public void TradeBook(Guid bookId, User owner, TradeRequest trade)
    {
        AddOwnerToBook(bookId, owner);
        AddTradeToBook(bookId, trade);
    }

    public void AddOwnerToBook(Guid bookId, User owner)
    {
        var bookToUpdate = GetBookById(bookId);

        bookToUpdate.Users.Add(owner);

        _context.SaveChanges();
    }

    public void AddTradeToBook(Guid bookId, TradeRequest trade)
    {
        var bookToUpdate = GetBookById(bookId);

        bookToUpdate.TradeRequests.Add(trade);

        _context.SaveChanges();
    }
}
