using BooksAPI.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleBackend.Models.Requests;
using SampleBackend.Models.Responses;

namespace BooksAPI.Controllers;
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

    [HttpGet("{id}")]
    public ActionResult<BookFullResponse> GetBookById([FromRoute] Guid id)
    {
        var book = new BookFullResponse();//filter by book id
        return Ok(book);
    }

    [HttpGet("search-genre-{genre}")]
    public ActionResult<List<BookShortResponse>> GetBooksByGenre([FromRoute] string genre)
    {
        var books = new List<BookShortResponse>();//filter by book genre
        return Ok(books);
    }

    [HttpGet("search-name-{name}")]
    public ActionResult<List<BookShortResponse>> GetBooksByName([FromRoute] string name)
    {
        var books = new List<BookShortResponse>();//filter by book genre
        return Ok(books);
    }

    [HttpGet("search-author-{author}")]
    public ActionResult<List<BookShortResponse>> GetBooksByAuthor([FromRoute] string author)
    {
        var books = new List<BookShortResponse>();//filter by book author
        return Ok(books);
    }

    [HttpGet("search-user-{userId}")]
    public ActionResult<List<BookShortResponse>> GetBooksByUserId([FromRoute] Guid userId)
    {
        var books = new List<BookShortResponse>();//filter by user id
        return Ok(books);
    }

    [HttpGet]
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
