using BooksAPI.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;
using AutoMapper;
using Books.BLL.Services.Interfaces;
using Books.BLL.Services;
using BooksAPI.Mappings;
using Books.BLL.Models;

namespace BooksAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/books")]
public class BooksController : Controller
{
    private IBooksService _service;

    private readonly Mapper _mapper;

    public BooksController()
    {
        var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new BookMapperProfile());
                });
        _mapper = new Mapper(config);

        _service = new BooksService();
    }

    [HttpPost, AllowAnonymous]
    public ActionResult<Guid> AddBook([FromBody] CreateBookRequest request)
    {
        var addedBookId = _service.AddBook(_mapper.Map<CreateBookModel>(request));
        return Ok(addedBookId);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<BookFullResponse> GetBookById([FromRoute] Guid id)
    {
        var book = _mapper.Map<BookFullModel>(_service.GetBookById(id));
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

    [HttpPatch("{id}"), AllowAnonymous]
    public IActionResult UpdateBook([FromRoute] Guid id, [FromBody] UpdateBookRequest request)
    {
        _service.UpdateBook(id, _mapper.Map<UpdateBookModel>(request));
        return NoContent();
    }

    [HttpDelete("{id}"), AllowAnonymous]
    public IActionResult DeleteBook([FromRoute] Guid id)
    {
        _service.DeleteBook(id);
        return NoContent();
    }

    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateBook([FromRoute] Guid id)
    {
        return NoContent();
    }
}
