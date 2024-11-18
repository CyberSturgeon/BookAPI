﻿using Books.Core;

namespace Books.DAL.DTOs;

public class TradeRequest
{
    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    public Guid UserId { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
