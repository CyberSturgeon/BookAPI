using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BooksAPI.Models.Requests;
using BooksAPI.Models.Responses;
using Books.BLL.Services.Interfaces;
using AutoMapper;
using Books.BLL.Models;
using Books.Core;

namespace BooksAPI.Controllers;
[ApiController]
[Route("api/trade-requests")]
[Authorize]
public class TradeRequestsController(
        ITradesService tradesService,
        IUsersService usersService,
        IBooksService booksService,
        IMapper mapper) : Controller
{

    [HttpPost(), AllowAnonymous]
    public ActionResult<Guid> AddTradeToBook([FromBody] TradeRequestRequest request)
    {
        var tradeId = tradesService.AddTradeToBook(mapper.Map<TradeRequestModel>(request));
        return Ok(tradeId);
    }

    [HttpGet("{id}"), AllowAnonymous]
    public ActionResult<TradeRequestResponse> GetTradeRequestById([FromRoute] Guid id)
    {
        var tradeRequest = mapper.Map<TradeRequestResponse>(tradesService.GetTradeById(id));
        return Ok(tradeRequest);
    }

    [HttpPatch("{id}/status"), AllowAnonymous]
    public IActionResult UpdateTradeRequestStatus([FromRoute] Guid id, [FromBody] TradeRequestStatus status)
    {
        var trade = tradesService.GetTradeById(id);

        if (trade.TradeStatus != TradeRequestStatus.Accepted)
        {
            if (status == TradeRequestStatus.Accepted)
            {
                booksService.AddUserToBook(trade.Buyer.Id, trade.Book.Id);
                booksService.AddUserToBook(trade.Owner.Id, trade.BookOffer.Id);

                usersService.RemoveBookFromUser(trade.Buyer.Id, trade.BookOffer.Id);
                usersService.RemoveBookFromUser(trade.Owner.Id, trade.Book.Id);

                tradesService.AcceptTrade(id);
            }
            else
            {
                tradesService.UpdateTradeStatus(id, status);
            }
        }
        return NoContent();
    }

    //[HttpDelete("{id}")]
    //public IActionResult DeleteTradeRequest([FromRoute] Guid id)
    //{
    //    return NoContent();
    //}
}
