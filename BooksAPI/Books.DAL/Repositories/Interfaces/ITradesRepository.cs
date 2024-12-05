using Books.Core;
using Books.DAL.DTOs;

namespace Books.DAL.Repositories.Interfaces
{
    public interface ITradesRepository
    {
        Guid AddTrade(TradeRequest trade);
        void DeleteTrade(TradeRequest trade);
        void DeleteTrades(List<TradeRequest> trades);
        TradeRequest? GetFullTradeById(Guid id);
        TradeRequest? GetTradeById(Guid id);
        ICollection<TradeRequest>? GetTradesByBookId(Guid bookId);
        ICollection<TradeRequest> GetTradesByUserId(Guid userId);
        void UpdateTrade(TradeRequest trade, TradeRequest newTrade);
        void UpdateTradeStatus(TradeRequest trade, TradeRequestStatus status);
    }
}