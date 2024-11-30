using BooksAPI.Models.Enums;

namespace BooksAPI.Models.Responses;

public class TradeRequestResponse
{
    public Guid BookId { get; set; }

    public Guid OwnerId { get; set; }

    public Guid BuyerId { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
