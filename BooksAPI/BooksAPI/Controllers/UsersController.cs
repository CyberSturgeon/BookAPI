using Books.BLL.Services;
using Books.BLL.Services.Interfaces;
using BooksAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleBackend.Models.Requests;
using SampleBackend.Models.Responses;

namespace SampleBackend.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : Controller
{
    private IUsersService _manager;

    public UsersController()
    {
        _manager = new UsersService();
    }

    [HttpPost, AllowAnonymous]
    public ActionResult<Guid> Register([FromBody] RegisterUserRequest request)
    {
        var addedUserId = Guid.NewGuid();
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

        var user = _manager.VerifyUser(request.Email, request.Password);
        var token = _manager.LogIn(user);

        if (token != null)
        {
            return Ok(new AuthenticatedResponse { Token = token });
        }
        else
        {
            return Unauthorized();
        }
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
        var user = new UserFullResponse();
        return Ok(user);
    }

    [HttpGet, AllowAnonymous]
    public ActionResult<List<UserResponse>> GetUsers()
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        return NoContent();
    }

    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateUser([FromRoute] Guid id)
    {
        return NoContent();
    }
}
