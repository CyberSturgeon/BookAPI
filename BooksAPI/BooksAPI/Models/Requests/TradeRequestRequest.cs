using Books.Core;
using BooksAPI.Models.Responses;

namespace BooksAPI.Models.Requests;

public class TradeRequestRequest
{
    public BookShortResponse Book { get; set; }

    public BookShortResponse BookOffer { get; set; }

    public UserResponse Owner { get; set; }

    public UserResponse Buyer { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
