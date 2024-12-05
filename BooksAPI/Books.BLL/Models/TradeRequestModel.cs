

using Books.Core;

namespace Books.BLL.Models;

public class TradeRequestModel
{
    public Guid Id { get; set; }

    public string? TradeDate { get; set; }

    public Guid BookId { get; set; }

    public Guid BookOfferId { get; set; }

    public Guid OwnerId { get; set; }

    public Guid BuyerId { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
