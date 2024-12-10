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
public class UsersController(
        IUsersService usersService,
        ITradesService tradesService,
        IMapper mapper) : Controller
{

    [HttpPost, AllowAnonymous]
    public ActionResult<Guid> Register([FromBody] RegisterUserRequest request)
    {
        var userToCreate = mapper.Map<CreateUserModel>(request);
        var addedUserId = usersService.AddUser(userToCreate);

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

        var token = usersService.VerifyUser(request.Email, request.Password);

        return Ok(new AuthenticatedResponse { Token = token });
    }

    [HttpGet("{id}/trades"), AllowAnonymous]
    public ActionResult<List<TradeRequestResponse>> GetTradeRequestsByUserId([FromRoute] Guid id)
    {
        var tradeRequests = mapper.Map<List<TradeRequestResponse>>(tradesService.GetTradesByUserId(id));
        return Ok(tradeRequests);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<UserFullResponse> GetUserById([FromRoute] Guid id)
    {
        var user = mapper.Map<UserFullResponse>(usersService.GetUserById(id));

        return Ok(user);
    }

    [HttpGet, AllowAnonymous]
    public ActionResult<ICollection<UserResponse>> GetUsers()
    {
        var users = mapper.Map<List<UserResponse>>(usersService.GetAllUsers());    

        return Ok(users);
    }

    [HttpPut("{id}"), AllowAnonymous]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        var userToUpdate = mapper.Map<UpdateUserModel>(request);
        usersService.UpdateUser(id, userToUpdate);

        return NoContent();
    }

    [HttpDelete("{id}"), AllowAnonymous]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        //usersService.DeleteUser(id);

        return NoContent();
    }

    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateUser([FromRoute] Guid id)
    {
        return NoContent();
    }

    [HttpGet("{id}/books"), AllowAnonymous]
    public ActionResult<List<BookShortResponse>> GetBooksByUserId([FromRoute] Guid userId)
    {
        var books = new List<BookShortResponse>();//filter by user id
        return Ok(books);
    }
}

