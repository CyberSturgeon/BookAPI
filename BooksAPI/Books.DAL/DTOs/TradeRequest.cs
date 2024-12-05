using Books.Core;

namespace Books.DAL.DTOs;

public class TradeRequest
{
    public Guid Id { get; set; }

    public string? TradeDate { get; set; }

    public Book? Book { get; set; }

    public Book? BookOffer { get; set; }

    public User? Owner { get; set; }

    public User? Buyer { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
