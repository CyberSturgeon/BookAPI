using AutoMapper;
using Books.BLL.Services;
using Books.BLL.Services.Interfaces;
using BooksAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BooksAPI.Models.Requests;
using Books.BLL.Models;
using BooksAPI.Mappings;

namespace BooksAPI.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : Controller
{
    private IUsersService _usersService;

    private IBooksService _booksService;

    private readonly Mapper _mapper;

    public UsersController(IUsersService usersService)
    {
        var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new BookMapperProfile());
                    cfg.AddProfile(new UserMapperProfile());
                });
        _mapper = new Mapper(config);

<<<<<<< HEAD
        _usersService = new UsersService();
        _booksService = new BooksService();
=======
        _service = usersService;
>>>>>>> di-experiments
    }

    [HttpPost, AllowAnonymous]
    public ActionResult<Guid> Register([FromBody] RegisterUserRequest request)
    {
        if (request is null)
        {
            return BadRequest("The registry request is bad.");
        }

        var userToCreate = _mapper.Map<CreateUserModel>(request);
        var addedUserId = _usersService.AddUser(userToCreate);

        return Ok(addedUserId);
    }

    // "api/users/login"
    [HttpPost("login"), AllowAnonymous]
    public IActionResult LogIn([FromBody] LoginRequest request)
    {
        if (request is null)
        {
            return BadRequest("The login request is bad.");
        }

        var token = _usersService.VerifyUser(request.Email, request.Password);

        return Ok(new AuthenticatedResponse { Token = token });
    }

    [HttpGet("{id}/trades")]
    public ActionResult<List<TradeRequestResponse>> GetTradeRequestsByUserId([FromRoute] Guid id)
    {
        var tradeRequests = new List<TradeRequestResponse>();
        return Ok(tradeRequests);
    }

    [HttpGet("{userId}/books"), AllowAnonymous]
    public ActionResult<List<BookShortResponse>> GetBooksByUserId([FromRoute] Guid userId)
    {
        var books = _mapper.Map<List<BookShortResponse>>(_usersService.GetUserById(userId).Books);

        return Ok(books);
    }


    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<UserFullResponse> GetUserById([FromRoute] Guid id)
    {
        var user = _mapper.Map<UserFullResponse>(_usersService.GetUserById(id));

        return Ok(user);
    }

    [HttpGet, AllowAnonymous]
    public ActionResult<ICollection<UserResponse>> GetUsers()
    {
        var users = _mapper.Map<List<UserResponse>>(_usersService.GetAllUsers());    

        return Ok(users);
    }

    [HttpPut("{id}"), AllowAnonymous]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        var userToUpdate = _mapper.Map<UpdateUserModel>(request);
        _usersService.UpdateUser(id, userToUpdate);

        return NoContent();
    }

    [HttpDelete("{id}"), AllowAnonymous]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        _usersService.DeleteUser(id);

        return NoContent();
    }

    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateUser([FromRoute] Guid id)
    {
        return NoContent();
    }
}
