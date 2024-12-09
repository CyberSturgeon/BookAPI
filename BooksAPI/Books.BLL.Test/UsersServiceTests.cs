using AutoMapper;
using Books.BLL.Exceptions;
using Books.BLL.Mappings;
using Books.BLL.Models;
using Books.BLL.Services;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;
using Moq;


namespace Books.BLL.Test;

public class UsersServiceTests
{
    private readonly Mock<ITradesRepository> _tradesRepositoryMock;
    private readonly Mock<IBooksRepository> _booksRepositoryMock;
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly Mapper _mapper;
    private readonly UsersService _sut;

    public UsersServiceTests()
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
    public void VerifyUser_WrongEmail_WrongLoginExceprionTrown()
    {
        var email = "yasha@lava.com";
        var msg = $"Wrong Email or password, try again";

        var ex = Assert.Throws<WrongLoginException>(() => _sut.VerifyUser(email, "test"));

        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void VerifyUser_GoodEmailWrongPassword_WrongLoginExceprionTrown()
    {
        var email = "yasha@lava.com";
        var msg = $"Wrong Email or password, try again";

        var ex = Assert.Throws<WrongLoginException>(() => _sut.VerifyUser(email, "test"));

        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void VerifyUser_GoodEmailGoodPassword_LoginSucess()
    {
        var email = "yasha@lava.com";
        var msg = $"Wrong Email or password, try again";

        _usersRepositoryMock.Setup(t => t.VerifyUser(email, "test")).Returns(new User { Email = email, Password = "test", Name = "test" });

        var token = _sut.VerifyUser(email, "test");

        Assert.NotEmpty(token);
    }

    [Fact]
    public void GetUserById_NonExistsUserId_EntityNotFoundExceprionTrown()
    {
        var userId = Guid.NewGuid();
        var msg = $"User {userId} not found";

        var ex = Assert.Throws<EntityNotFoundException>(() => _sut.GetUserById(userId));

        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void GetUserById_ExistsUserId_GetUserSuccess()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId };
        var userModel = _mapper.Map<UserFullModel>(user);
        var trades = new List<TradeRequest>{ new TradeRequest { Owner = user } };
        userModel.Trades = _mapper.Map<List<TradeModel>>(trades);

        _usersRepositoryMock.Setup(t => t.GetUserFullProfileById(userId)).Returns(user);
        _tradesRepositoryMock.Setup(t => t.GetTradesByUserId(userId)).Returns(trades);

        var userModelReturned = _sut.GetUserById(userId);

        Assert.Equivalent(userModel, userModelReturned);
    }

    [Fact]
    public void DeleteUser_NonExistsUserId_EntityNotFoundExceprionTrown()
    {
        var userId = Guid.NewGuid();
        var msg = $"User {userId} not found";

        var ex = Assert.Throws<EntityNotFoundException>(() => _sut.DeleteUser(userId));

        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void DeleteUser_ExistsUserId_DeleteSucess()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId };
      
        _usersRepositoryMock.Setup(t => t.GetUserById(userId)).Returns(user);

        _sut.DeleteUser(userId);

        _usersRepositoryMock.Verify(t => t.DeleteUser(user));
    }

    [Fact]
    public void AddUser_ExistsUserEmail_EntityConflictExceptionTrown()
    {
        var email = "yasha@lava.com";
        var createModel = new CreateUserModel { Email = email };
        var user = new User { Email = email };
        var msg = $"User {email} already exists";

        _usersRepositoryMock.Setup(t => t.GetUserByEmail(email)).Returns(user);

        var ex = Assert.Throws<EntityConflictException>(() => _sut.AddUser(createModel));

        Assert.Equal(msg, ex.Message);
    }

    [Fact]
    public void AddUser_NonExistsUserEmail_AddSucess()
    {
        var email = "yasha@lava.com";
        var createModel = new CreateUserModel { Email = email };
        var id = Guid.NewGuid();
        var user = new User { Id = id, Email = email };

        _usersRepositoryMock.Setup(t => t.AddUser(user)).Returns(id);

        var userId = _sut.AddUser(createModel);

        _usersRepositoryMock.Verify(t => t.AddUser(It.IsAny<User>()));
    }
}
