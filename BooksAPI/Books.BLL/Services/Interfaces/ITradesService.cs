using Books.BLL.Models;
using Books.Core;

namespace Books.BLL.Services.Interfaces
{
    public interface ITradesService
    {
        Guid AddTradeToBook(TradeRequestModel tradeModel);
        void UpdateTradeStatus(Guid tradeId, TradeRequestStatus status);
        TradeModel GetTradeById(Guid tradeId);
        ICollection<TradeModel> GetTradesByUserId(Guid userId);
        ICollection<TradeModel> GetTradesByBookId(Guid bookId);
        void UpdateTrade(Guid tradeId, TradeRequestStatus status);
    }
}