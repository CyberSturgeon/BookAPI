using AutoMapper;
using Books.BLL.Exceptions;
using Books.BLL.Mappings;
using Books.BLL.Models;
using Books.BLL.Services;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;
using Moq;

namespace Books.BLL.Test;

public class BooksServiceTests
{
    private readonly Mock<ITradesRepository> _tradesRepositoryMock;
    private readonly Mock<IBooksRepository> _booksRepositoryMock;
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly Mapper _mapper;
    private readonly BooksService _sut;

    public BooksServiceTests()
    {
        _tradesRepositoryMock = new Mock<ITradesRepository>();
        _booksRepositoryMock = new Mock<IBooksRepository>();
        _usersRepositoryMock = new Mock<IUsersRepository>();

        var config = new MapperConfiguration(
        cfg =>
            {
                cfg.AddProfile(new BookMapperProfile());
                cfg.AddProfile(new TradeMapperProfile());
                cfg.AddProfile(new UserMapperProfile());

            });

        _mapper = new(config);

        _sut = new(_booksRepositoryMock.Object,
                _usersRepositoryMock.Object,
                _tradesRepositoryMock.Object,
                _mapper);
    }

    [Fact]
    public void AddBook_NonExistingUser_EntityNotFoundExceptionTrown()
    {
        var userId = Guid.NewGuid();
        var bookModel = new CreateBookModel{ UserId = userId};
        var msg = $"User {bookModel.UserId} not found";

        var ex = Assert.Throws<EntityNotFoundException>( () => _sut.AddBook(bookModel));

        Assert.Equal(msg, ex.Message);

    }

    [Fact]
    public void AddBook_ExistingUser_CreatingBookSuccess()
    {
        var userId = Guid.NewGuid();
        var bookId = Guid.NewGuid();
        var user = new User { Id = userId };
        var book = new Book { Users = new List<User> { user } };
        var bookModel = new CreateBookModel { UserId = userId };

        _usersRepositoryMock.Setup(t => t.GetUserById(userId)).Returns(user);
        _booksRepositoryMock.Setup(t => t.AddBook(book, user)).Returns(bookId);

        _sut.AddBook(bookModel);

        _booksRepositoryMock.Verify(t => t.AddBook(It.IsAny<Book>(), It.IsAny<User>()), Times.Once);

    }

    [Fact]
    public void GetBookById_NonExistsBookId_EntityNotFoundExceptionTrown()
    {
        var bookId = Guid.NewGuid();
        var msg = $"Book {bookId} not found";

        var ex = Assert.Throws<EntityNotFoundException>(() => _sut.GetBookById(bookId));

        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void GetBookById_ExistsBookId_GetBookSuccess()
    {
        var bookId = Guid.NewGuid();
        _booksRepositoryMock.Setup(t => t.GetBookFullProfileById(bookId)).Returns(new Book { Id = bookId});

        _sut.GetBookById(bookId);
        
        _booksRepositoryMock.Verify(t => t.GetBookFullProfileById(bookId), Times.Once);   
        _tradesRepositoryMock.Verify(t => t.GetTradesByBookId(bookId), Times.Once);
    }

    [Fact]
    public void DeleteBook_NonExistsBookId_EntityNotFoundExceptionTrown()
    {
        var bookId = Guid.NewGuid();
        var msg = $"Book {bookId} not found";

        var ex = Assert.Throws<EntityNotFoundException>(() => _sut.DeleteBook(bookId));

        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void DeleteBook_ExistsBookId_DeleteBookSuccess()
    {
        var bookId = Guid.NewGuid();
        var book = new Book { Id = bookId };
        _booksRepositoryMock.Setup(t => t.GetBookById(bookId)).Returns(book);

        _sut.DeleteBook(bookId);

        _booksRepositoryMock.Verify(t => t.DeleteBook(book), Times.Once);
    }

}