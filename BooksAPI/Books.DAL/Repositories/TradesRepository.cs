using Books.Core;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.DAL.Repositories;

public class TradesRepository(BooksContext context) : ITradesRepository
{
    public TradeRequest? GetTradeById(Guid id)
    {
        return context.TradeRequests.Where(t => t.Id == id).FirstOrDefault();
    }

    public TradeRequest? GetFullTradeById(Guid id)
    {
        return context.TradeRequests.Where(t => t.Id == id)
            .Include(t => t.Buyer)
            .Include(t => t.Owner)
            .Include(t => t.Book)
            .Include(t => t.BookOffer)
            .FirstOrDefault();
    }

    public ICollection<TradeRequest>? GetTradesByOwnerId(Guid ownerId)
    {
        return context.TradeRequests.Where(t => t.Owner.Id == ownerId).ToList();
    }

    public ICollection<TradeRequest>? GetTradesByBuyerId(Guid buyerId)
    {
        return context.TradeRequests.Where(t => t.Buyer.Id == buyerId).ToList();
    }

    public ICollection<TradeRequest>? GetTradesByBookId(Guid bookId)
    {
        return context.TradeRequests.Where(t => t.Book.Id == bookId).ToList();
    }

    public void DeleteTrades(List<TradeRequest> trades)
    {
        context.TradeRequests.RemoveRange(trades);
    }

    public void DeleteTrade(TradeRequest trade)
    {
        context.TradeRequests.Remove(trade);
        context.SaveChanges();
    }

    public void UpdateTrade(TradeRequest trade, TradeRequest newTrade)
    {
        trade.Owner = newTrade.Owner;
        trade.Buyer = newTrade.Buyer;
        trade.Book = newTrade.Book;

        context.SaveChanges();
    }

    public void UpdateTradeStatus(TradeRequest trade, TradeRequestStatus status)
    {
        trade.TradeStatus = status;

        context.SaveChanges();
    }

    public Guid AddTrade(TradeRequest trade)
    {
        context.TradeRequests.Add(trade);
        context.SaveChanges();

        return trade.Id;
    }
}
