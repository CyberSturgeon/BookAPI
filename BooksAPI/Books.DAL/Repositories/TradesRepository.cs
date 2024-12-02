using Books.Core;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;

namespace Books.DAL.Repositories;

public class TradesRepository(BooksContext context, IUsersRepository usersRepository) : ITradesRepository
{
    public TradeRequest? GetTradeById(Guid id)
    {
        return context.TradeRequests.Where(t => t.Id == id).FirstOrDefault();
    }

    public ICollection<TradeRequest>? GetTrades()
    {
        return context.TradeRequests.ToList();
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
