using BooksAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleBackend.Models.Requests;
using SampleBackend.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Books.BLL.Servicies;
using Books.Core;

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

        if (user != null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: Options.Issuer,
                audience: Options.Audience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthenticatedResponse { Token = tokenString });
        }
        else
        {
            return Unauthorized();
        }
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
