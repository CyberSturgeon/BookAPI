using Books.Core;
using Books.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.BLL.Models;

public class TradeModel
{
    public Guid Id { get; set; }

    public string TradeDate { get; set; }

    public BookModel Book { get; set; }

    public UserModel Owner { get; set; }

    public UserModel Buyer { get; set; }

    public TradeRequestStatus TradeStatus { get; set; }
}
