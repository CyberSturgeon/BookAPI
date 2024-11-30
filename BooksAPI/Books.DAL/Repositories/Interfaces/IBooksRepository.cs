using Books.DAL.DTOs;

namespace Books.DAL.Repositories.Interfaces
{
    public interface IBooksRepository
    {
        public Guid AddBook(Book book);
        public void DeleteBook(Book book);
        public Book? GetBookById(Guid id);
        public ICollection<Book>? GetBooks();
        public void UpdateBook(Book book, Book newBook);
        public ICollection<Book>? GetBooksByFilter(BookFilter filter);
        public Book? GetBookFullProfileById(Guid id);
        public void AddUserToBook(Book book, User owner);
        public void AddTradeToBook(Book book, TradeRequest trade);
    }
}