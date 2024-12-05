using Books.BLL.Models;

namespace Books.BLL.Services.Interfaces
{
    public interface IBooksService
    {
        Guid AddBook(CreateBookModel bookModel);
        void AddUserToBook(Guid bookId, Guid userId);
        void DeleteBook(Guid id);
        ICollection<BookModel> GetAllBooks();
        BookFullModel GetBookById(Guid id);
        void UpdateBook(Guid id, UpdateBookModel newBookModel);
        public List<BookModel> GetBooksByFilter(BookFilterModel filter);
    }
}