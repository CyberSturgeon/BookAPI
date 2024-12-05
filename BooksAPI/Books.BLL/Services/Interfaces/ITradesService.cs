using Books.BLL.Models;
using Books.Core;

namespace Books.BLL.Services.Interfaces
{
    public interface ITradesService
    {
        Guid AddTradeToBook(TradeModel tradeModel);
        void UpdateTradeStatus(Guid tradeId, TradeRequestStatus status);
        TradeModel GetTradeById(Guid tradeId);
    }
}