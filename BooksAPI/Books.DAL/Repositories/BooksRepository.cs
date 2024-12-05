using Books.DAL.DTOs;
using Microsoft.EntityFrameworkCore;
namespace Books.DAL.Repositories.Interfaces;

public class BooksRepository(BooksContext context) : IBooksRepository
{
    public Book? GetBookById(Guid id)
    {
        return context.Books.Where(b => b.Id == id).FirstOrDefault();
    }

    public Book? GetBookFullProfileById(Guid id)
    {
        return context.Books.Where(b => b.Id == id)
            .Include(b => b.Users)
            .Include(b => b.TradeRequests).FirstOrDefault();
    }

    public ICollection<Book>? GetBooks()
    {
        return context.Books.ToList();
    }

    public ICollection<Book>? GetBooksByFilter(BookFilter filter)
    {

        return context.Books.Where(b => string.IsNullOrEmpty(filter.Name) || b.Name == filter.Name &&
            string.IsNullOrEmpty(filter.Author) || b.Author == filter.Author &&
            string.IsNullOrEmpty(filter.Genre) || b.Genre == filter.Genre).ToList();
    }

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
