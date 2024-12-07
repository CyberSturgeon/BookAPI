using Books.DAL.DTOs;
using Microsoft.EntityFrameworkCore;
namespace Books.DAL.Repositories.Interfaces;

public class BooksRepository(BooksContext context) : IBooksRepository
{
    public Book? GetBookById(Guid id) 
        => context.Books
            .FirstOrDefault(b => b.Id == id);

    public Book? GetBookFullProfileById(Guid id)
        => context.Books
            .Include(b => b.Users).ThenInclude(u => u.Books)
            .Include(b => b.Users).ThenInclude(u => u.Trades)
            .Include(b => b.TradeRequests)
            .FirstOrDefault(b => b.Id == id);

    public ICollection<Book>? GetBooks()
        => context.Books.ToList();

    public ICollection<Book>? GetBooksByFilter(BookFilter filter)
        => context.Books
            .Where(b => string.IsNullOrEmpty(filter.Name) || 
                        b.Name == filter.Name &&
                        string.IsNullOrEmpty(filter.Author) ||
                        b.Author == filter.Author &&
                        string.IsNullOrEmpty(filter.Genre) || 
                        b.Genre == filter.Genre)
            .ToList();

    public void DeleteBook(Book book)
    {
        context.Books.Remove(book);
        context.SaveChanges();
    }

    public void UpdateBook(Book book, Book newBook)
    {
        book.Name = newBook.Name;
        book.Author = newBook.Author;
        book.Genre = newBook.Genre;

        context.SaveChanges();
    }

    public Guid AddBook(Book book, User owner)
    {
        book.TradeRequests = new List<TradeRequest>();
        book.Users = new List<User>();

        context.Books.Add(book);
        context.SaveChanges();

        return book.Id;
    }

    public void AddUserToBook(Book book, User owner)
    {
        book.Users.Add(owner);

        context.SaveChanges();
    }

    public void AddTradeToBook(Book book, TradeRequest trade)
    {
        book.TradeRequests.Add(trade);

        context.SaveChanges();
    }
}
