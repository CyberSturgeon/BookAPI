using Books.Core;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;

namespace Books.DAL.Repositories;

public class TradesRepository : ITradesRepository
{
    private BooksContext _context;

    private readonly IUsersRepository _usersRepository;

    public TradesRepository()
    {
        _usersRepository = new UsersRepository();

        _context = new BooksContext();
    }

    public TradeRequest? GetTradeById(Guid id)
    {
        return _context.TradeRequests.Where(t => t.Id == id).FirstOrDefault();
    }

    public ICollection<TradeRequest>? GetTrades()
    {
        return _context.TradeRequests.ToList();
    }

    public void DeleteTrade(TradeRequest trade)
    {
        _context.TradeRequests.Remove(trade);
        _context.SaveChanges();
    }

    public void UpdateTrade(TradeRequest trade, TradeRequest newTrade)
    {
        trade.Owner = newTrade.Owner;
        trade.Buyer = newTrade.Buyer;
        trade.Book = newTrade.Book;

        _context.SaveChanges();
    }

    public void UpdateTradeStatus(TradeRequest trade, TradeRequestStatus status)
    {
        trade.TradeStatus = status;

        _context.SaveChanges();
    }

    public Guid AddTrade(TradeRequest trade)
    {
        _context.TradeRequests.Add(trade);
        _context.SaveChanges();

        return trade.Id;
    }
}
