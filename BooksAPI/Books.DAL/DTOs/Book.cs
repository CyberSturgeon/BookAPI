using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DAL.DTOs;

public class Book
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public IEnumerable<TradeRequest>? TradeRequests { get; set; }

    public IEnumerable<User>? Users { get; set; }
}
