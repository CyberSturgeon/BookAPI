using Microsoft.AspNetCore.Mvc;
using SampleBackend.Models.Requests;
using SampleBackend.Models.Responses;

namespace BooksAPI.Controllers;
[ApiController]
[Route("api/trade-requests")]
public class TradeRequestsController : Controller
{
    [HttpPost]
    public ActionResult<Guid> AddTradeRequest([FromBody] RegisterUserRequest request)
    {
        var addedRequestId = Guid.NewGuid();
        return Ok(addedRequestId);
    }

    [HttpGet("{id}")]
    public ActionResult<TradeRequestResponse> GetTradeRequestById([FromRoute] Guid id)
    {
        var tradeRequest = new TradeRequestResponse();
        return Ok(tradeRequest);
    }

    [HttpGet("search-user-{id}")]
    public ActionResult<List<TradeRequestResponse>> GetTradeRequestsByUserId([FromRoute] Guid id)
    {
        var tradeRequests = new List<TradeRequestResponse>();
        return Ok(tradeRequests);
    }

    [HttpGet("search-book-{id}")]
    public ActionResult<List<TradeRequestResponse>> GetTradeRequestsByBookId([FromRoute] Guid id)
    {
        var tradeRequests = new List<TradeRequestResponse>();
        return Ok(tradeRequests);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateTradeRequest([FromRoute] Guid id, [FromBody] string status)
    {
        return NoContent();
    }

    [HttpPatch("deactivate-book-{id}")]
    public IActionResult DeactivateRequest([FromRoute] Guid id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTradeRequest([FromRoute] Guid id)
    {
        return NoContent();
    }
}
