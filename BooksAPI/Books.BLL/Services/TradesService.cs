using AutoMapper;
using Books.BLL.Exceptions;
using Books.BLL.Models;
using Books.BLL.Services.Interfaces;
using Books.Core;
using Books.DAL.DTOs;
using Books.DAL.Repositories.Interfaces;

namespace Books.BLL.Services;

public class TradesService(ITradesRepository tradesRepository,
        IUsersRepository usersRepository,
        IBooksRepository booksRepository,
        IMapper mapper) : ITradesService
{
    public Guid AddTradeToBook(TradeRequestModel tradeModel)
    {
        var bookId = tradeModel.BookId;
        var bookOfferId = tradeModel.BookOfferId;
        var ownerId = tradeModel.OwnerId;
        var buyerId = tradeModel.BuyerId;

        var book = booksRepository.GetBookFullProfileById(bookId) ??
            throw new EntityNotFoundException($"Book {bookId} not found");

        var bookOffer = booksRepository.GetBookFullProfileById(bookOfferId) ??
            throw new EntityNotFoundException($"Book to offer {bookOfferId} not found");

        var buyer = usersRepository.GetUserFullProfileById(buyerId) ??
            throw new EntityNotFoundException($"User buyer {buyerId} not found");

        var owner = usersRepository.GetUserFullProfileById(ownerId) ??
            throw new EntityNotFoundException($"User owner {ownerId} not found");

        CheckOwnership(book, owner);
        CheckOwnership(bookOffer, buyer);

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

    public void UpdateTrade(Guid tradeId, TradeRequestStatus status)
    {
        var trade = tradesRepository.GetFullTradeById(tradeId) ??
            throw new EntityNotFoundException($"Trade {tradeId} was not found");
        var tradeDate = DateTime.Now.ToString();

        tradesRepository.UpdateTrade(trade, tradeDate, status);
    }

    public void UpdateTradeStatus(Guid tradeId, TradeRequestStatus status)
    {
        var trade = tradesRepository.GetFullTradeById(tradeId) ??
            throw new EntityNotFoundException($"Trade {tradeId} was not found");

        tradesRepository.UpdateTradeStatus(trade, status);
    }

    public TradeModel GetTradeById(Guid tradeId) 
            => mapper.Map<TradeModel>(tradesRepository.GetFullTradeById(tradeId)) ??
                throw new EntityNotFoundException($"Trade {tradeId} was not found");

    public ICollection<TradeModel> GetTradesByUserId(Guid userId)
            => mapper.Map<List<TradeModel>>(tradesRepository.GetTradesByUserId(userId));

    public ICollection<TradeModel> GetTradesByBookId(Guid bookId)
            => mapper.Map<List<TradeModel>>(tradesRepository.GetTradesByBookId(bookId));
    
    private void CheckOwnership(Book book, User user)
    {
        if (book.Users.LastOrDefault().Id != user.Id)
            throw new EntityConflictException($"User {user.Id} no own book {book.Id}");
    }
}