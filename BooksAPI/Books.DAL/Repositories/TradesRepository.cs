using Books.Core;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Books.DAL.Repositories;

public class TradesRepository(BooksContext context) : ITradesRepository
{
    public TradeRequest? GetTradeById(Guid id) => context.TradeRequests
            .FirstOrDefault(t => t.Id == id);

    public TradeRequest? GetFullTradeById(Guid id) 
        => context.TradeRequests
            .Include(t => t.Buyer)
            .Include(t => t.Owner)
            .Include(t => t.Book)
            .Include(t => t.BookOffer)
            .FirstOrDefault(t => t.Id == id);

    public ICollection<TradeRequest> GetTradesByUserId(Guid userId)
        => context.TradeRequests
            .Where(t => t.Owner.Id == userId || t.Buyer.Id == userId)
            .Include(t => t.Buyer)
            .Include(t => t.Owner)
            .Include(t => t.Book)
            .Include(t => t.BookOffer)
            .ToList();

    public ICollection<TradeRequest> GetTradesByBookId(Guid bookId)
        => context.TradeRequests
            .Where(t => t.Book.Id == bookId || t.BookOffer.Id == bookId)
            .Include(t => t.Buyer)
            .Include(t => t.Owner)
            .Include(t => t.Book)
            .Include(t => t.BookOffer)
            .ToList();

    public void DeleteTrades(List<TradeRequest> trades)
    {
        context.TradeRequests.RemoveRange(trades);
    }

    public void DeleteTrade(TradeRequest trade)
    {
        context.TradeRequests.Remove(trade);
        context.SaveChanges();
    }

    public void UpdateTrade(TradeRequest trade, string tradeDate, TradeRequestStatus tradeStatus)
    {
        trade.TradeDate = tradeDate;
        trade.TradeStatus = tradeStatus;

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
