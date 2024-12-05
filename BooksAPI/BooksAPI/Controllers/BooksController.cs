using AutoMapper;
using Books.BLL.Models;
using Books.BLL.Services.Interfaces;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/books")]
public class BooksController(
        IBooksService booksService,
        IMapper mapper
    ) : Controller
{
    [HttpPost, AllowAnonymous]
    public ActionResult<Guid> AddBook([FromBody] CreateBookRequest request)
    {
        var addedBookId = booksService.AddBook(mapper.Map<CreateBookModel>(request));
        return Ok(addedBookId);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<BookFullResponse> GetBookById([FromRoute] Guid id)
    {
        var book = mapper.Map<BookFullModel>(booksService.GetBookById(id));
        return Ok(book);
    }

    [HttpPost("search"), AllowAnonymous]
    public ActionResult<List<BookShortResponse>> SearchBooks([FromBody] SearchBookRequest request)
    {
<<<<<<< HEAD
        var books = _mapper.Map<List<BookShortResponse>>(_service.GetBooksByFilter(_mapper.Map<BookFilterModel>(request)));
=======
        var books = mapper.Map<List<BookShortResponse>>(booksService.GetAllBooks());
>>>>>>> di-experiments
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
        var books = mapper.Map<List<BookShortResponse>>(booksService.GetAllBooks());//no filtering
        return Ok(books);
    }

    [HttpPatch("{id}"), AllowAnonymous]
    public IActionResult UpdateBook([FromRoute] Guid id, [FromBody] UpdateBookRequest request)
    {
        booksService.UpdateBook(id, mapper.Map<UpdateBookModel>(request));
        return NoContent();
    }

    [HttpDelete("{id}"), AllowAnonymous]
    public IActionResult DeleteBook([FromRoute] Guid id)
    {
        booksService.DeleteBook(id);
        return NoContent();
    }

    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateBook([FromRoute] Guid id)
    {
        return NoContent();
    }
}
