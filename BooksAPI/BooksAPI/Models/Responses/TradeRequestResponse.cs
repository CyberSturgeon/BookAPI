using BooksAPI.Models.Enums;

namespace SampleBackend.Models.Responses;

public class TradeRequestResponse
{
    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    public Guid UserId { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
