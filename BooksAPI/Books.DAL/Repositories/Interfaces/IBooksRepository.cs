using Books.DAL.DTOs;

namespace Books.DAL.Interfaces
{
    public interface IBooksRepository
    {
        Guid AddBook(Book book);
        void AddOwnerToBook(Guid bookId, User owner);
        void AddTradeToBook(Guid bookId, TradeRequest trade);
        void DeleteBook(Book book);
        Book? GetBookById(Guid id);
        ICollection<Book>? GetBooks();
        void TradeBook(Guid bookId, User owner, TradeRequest trade);
        void UpdateBook(Book book);
    }
}