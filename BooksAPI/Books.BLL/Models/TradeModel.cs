using Books.Core;

namespace Books.BLL.Models;

public class TradeModel
{
    public Guid Id { get; set; }

    public string? TradeDate { get; set; }

    public BookModel? Book { get; set; }

    public BookModel? BookOffer { get; set; }

    public UserModel? Owner { get; set; }

    public UserModel? Buyer { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
