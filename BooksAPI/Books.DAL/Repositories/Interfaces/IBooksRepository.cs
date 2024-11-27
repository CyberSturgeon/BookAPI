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
        public ICollection<Book>? GetBooksByFilter(BooksFilter filter);
    }
}