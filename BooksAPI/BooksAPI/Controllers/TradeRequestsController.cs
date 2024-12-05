using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;
using Books.BLL.Services.Interfaces;
using AutoMapper;
using Books.BLL.Models;

namespace BooksAPI.Controllers;
[ApiController]
[Route("api/trade-requests")]
[Authorize]
public class TradeRequestsController(
        ITradesService tradesService,
        IMapper mapper) : Controller
{
    [HttpPost, AllowAnonymous]
    public ActionResult<Guid> AddTradeRequest([FromBody] TradeRequestRequest request)
    {
        var addedRequestId = tradesService.AddTradeToBook(mapper.Map<TradeModel>(request));
        return Ok(addedRequestId);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<TradeRequestResponse> GetTradeRequestById([FromRoute] Guid id)
    {
        var tradeRequest = mapper.Map<TradeRequestResponse>(tradesService.GetTradeById(id));
        return Ok(tradeRequest);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateTradeRequest([FromRoute] Guid id, [FromBody] string status)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTradeRequest([FromRoute] Guid id)
    {
        return NoContent();
    }
}
