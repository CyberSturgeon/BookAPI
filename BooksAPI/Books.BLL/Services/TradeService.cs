using AutoMapper;
using Books.BLL.Exceptions;
using Books.BLL.Models;
using Books.Core;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;

namespace Books.BLL.Services;

public class TradesService(ITradesRepository tradesRepository,
        IUsersRepository usersRepository,
        IBooksRepository booksRepository,
        IMapper mapper)
{

    public Guid AddTradeToBook(TradeModel tradeModel)
    {
        var bookId = tradeModel.Book.Id;
        var bookOfferId = tradeModel.BookOffer.Id;
        var ownerId = tradeModel.Owner.Id;
        var buyerId = tradeModel.Buyer.Id;

        var book = booksRepository.GetBookById(bookId) ??
            throw new EntityNotFoundException($"Book {bookId} not found");

        var bookOffer = booksRepository.GetBookById(bookOfferId) ??
            throw new EntityNotFoundException($"Book to offer {bookOfferId} not found");

        var buyer = usersRepository.GetUserById(buyerId) ??
            throw new EntityNotFoundException($"User buyer {buyerId} not found");

        var owner = usersRepository.GetUserById(ownerId) ??
            throw new EntityNotFoundException($"User owner {ownerId} not found");

        var trade = new TradeRequest
        {
            Book = book,
            BookOffer = bookOffer,
            Buyer = buyer,
            Owner = owner,
            TradeStatus = Core.TradeRequestStatus.Waiting,
        };

        var tradeId = tradesRepository.AddTrade(trade);

        return tradeId;
    }

    public void UpdateTradeStatus(Guid tradeId, TradeRequestStatus status)
    {
        var trade = tradesRepository.GetTradeById(tradeId) ??
            throw new EntityNotFoundException($"Trade {tradeId} was not found");

        tradesRepository.UpdateTradeStatus(trade, status);
    }
}