using Microsoft.AspNetCore.Mvc;
using SampleBackend.Models.Requests;
using SampleBackend.Models.Responses;

namespace SampleBackend.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : Controller
{
    [HttpPost]
    public ActionResult<Guid> Register([FromBody] RegisterUserRequest request)
    {
        var addedUserId = Guid.NewGuid();
        return Ok(addedUserId);
    }

    // "api/users/login"
    [HttpPost("login")]
    public IActionResult LogIn([FromBody] LoginRequest request)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public ActionResult<UserFullResponse> GetUserById([FromRoute] Guid id)
    {
        var user = new UserFullResponse();
        return Ok(user);
    }

    [HttpGet]
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
