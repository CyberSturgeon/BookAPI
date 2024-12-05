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
    private IUsersService _service;

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

        _service = usersService;
    }

    [HttpPost, AllowAnonymous]
    public ActionResult<Guid> Register([FromBody] RegisterUserRequest request)
    {
        var userToCreate = _mapper.Map<CreateUserModel>(request);
        var addedUserId = _service.AddUser(userToCreate);

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

        var token = _service.VerifyUser(request.Email, request.Password);

        return Ok(new AuthenticatedResponse { Token = token });
    }

    [HttpGet("{id}/trades")]
    public ActionResult<List<TradeRequestResponse>> GetTradeRequestsByUserId([FromRoute] Guid id)
    {
        var tradeRequests = new List<TradeRequestResponse>();
        return Ok(tradeRequests);
    }

    [HttpGet("{id}/books"), AllowAnonymous]
    public ActionResult<List<BookShortResponse>> GetBooksByUserId([FromRoute] Guid userId)
    {
        var books = new List<BookShortResponse>();//filter by user id
        return Ok(books);
    }


    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<UserFullResponse> GetUserById([FromRoute] Guid id)
    {
        var user = _mapper.Map<UserFullResponse>(_service.GetUserById(id));

        return Ok(user);
    }

    [HttpGet, AllowAnonymous]
    public ActionResult<ICollection<UserResponse>> GetUsers()
    {
        var users = _mapper.Map<List<UserResponse>>(_service.GetAllUsers());    

        return Ok(users);
    }

    [HttpPut("{id}"), AllowAnonymous]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        var userToUpdate = _mapper.Map<UpdateUserModel>(request);
        _service.UpdateUser(id, userToUpdate);

        return NoContent();
    }

    [HttpDelete("{id}"), AllowAnonymous]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        _service.DeleteUser(id);

        return NoContent();
    }

    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateUser([FromRoute] Guid id)
    {
        return NoContent();
    }
}
