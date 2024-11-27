using Books.Core;
using Books.DAL.DTOs;

namespace Books.DAL.Repositories.Interfaces
{
    public interface ITradesRepository
    {
        public Guid AddTrade(TradeRequest trade);
        public void DeleteTrade(TradeRequest trade);
        public TradeRequest? GetTradeById(Guid id);
        public ICollection<TradeRequest>? GetTrades();
        public void UpdateTrade(TradeRequest trade, TradeRequest newTrade);
        public void UpdateTradeStatus(TradeRequest trade, TradeRequestStatus status);
    }
}