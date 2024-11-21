using BooksAPI.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleBackend.Models.Requests;
using SampleBackend.Models.Responses;

namespace BooksAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/books")]
public class BooksController : Controller
{
    [HttpPost]
    public ActionResult<Guid> AddBook([FromBody] CreateBookRequest request)
    {
        var addedBookId = Guid.NewGuid();
        return Ok(addedBookId);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<BookFullResponse> GetBookById([FromRoute] Guid id)
    {
        var book = new BookFullResponse();//filter by book id
        return Ok(book);
    }

    [HttpPost("search"), AllowAnonymous]
    public ActionResult<List<BookShortResponse>> SearchBooks([FromBody] SearchBookRequest request)
    {
        var books = new List<BookShortResponse>();
        return Ok(books);
    }

    [HttpGet("{id}/trades")]
    public ActionResult<List<TradeRequestResponse>> GetTradeRequestsByBookId([FromRoute] Guid id)
    {
        var tradeRequests = new List<TradeRequestResponse>();
        return Ok(tradeRequests);
    }

    [HttpDelete("{id}/trades")]
    public IActionResult DeactivateRequest([FromRoute] Guid id)
    {
        return NoContent();
    }

    [HttpGet, AllowAnonymous]
    public ActionResult<List<BookShortResponse>> GetBooks()
    {
        var books = new List<BookShortResponse>();//no filtering
        return Ok(books);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateBook([FromRoute] Guid id, [FromBody] UpdateBookRequest request)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook([FromRoute] Guid id)
    {
        return NoContent();
    }

    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateBook([FromRoute] Guid id)
    {
        return NoContent();
    }
}
