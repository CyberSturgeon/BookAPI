using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleBackend.Models.Requests;
using SampleBackend.Models.Responses;

namespace BooksAPI.Controllers;
[ApiController]
[Route("api/trade-requests")]
[Authorize]
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
