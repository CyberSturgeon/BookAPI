using Books.Core;

namespace BooksAPI.Models.Responses;

public class TradeRequestResponse
{
    public BookShortResponse Book { get; set; }

    public BookShortResponse BookOffer { get; set; }

    public string TradeDate { get; set; }

    public UserResponse Owner { get; set; }

    public UserResponse Buyer { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
