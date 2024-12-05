using AutoMapper;
using Books.DAL.Repositories.Interfaces;
using Books.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.BLL.Mappings;
using Books.BLL.Exceptions;
using Books.DAL.DTOs;
using Books.Core;

namespace Books.BLL.Services;

public class TradesService
{
    private IBooksRepository _booksRepository;

    private IUsersRepository _usersRepository;

    private ITradesRepository _tradesRepository;

    private readonly Mapper _mapper;

    public TradesService()
    {
        _booksRepository = new BooksRepository();

        _usersRepository = new UsersRepository();

        _tradesRepository = new TradesRepository();

        var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile(new UserMapperProfile());
                    cfg.AddProfile(new BookMapperProfile());
                    cfg.AddProfile(new TradeMapperProfile());
                });
        _mapper = new Mapper(config);
    }

    public Guid AddTradeToBook(Guid bookId, Guid buyerId, Guid ownerId)
    {
        var book = _booksRepository.GetBookById(bookId) ??
            throw new EntityNotFoundException($"Book {bookId} not found");

        var buyer = _usersRepository.GetUserById(buyerId) ??
            throw new EntityNotFoundException($"User buyer {buyerId} not found");

        var owner = _usersRepository.GetUserById(ownerId) ??
            throw new EntityNotFoundException($"User owner {ownerId} not found");

        var trade = new TradeRequest
        {
            Book = book,
            Buyer = buyer,
            Owner = owner,
            TradeStatus = Core.TradeRequestStatus.Waiting,
        };

        var tradeId = _tradesRepository.AddTrade(trade);

        return tradeId;
    }

    public void UpdateTradeStatus(Guid tradeId, TradeRequestStatus status)
    {
        var trade = _tradesRepository.GetTradeById(tradeId) ??
            throw new EntityNotFoundException($"Trade {tradeId} was not found");

        _tradesRepository.UpdateTradeStatus(trade, status);
    }
}
