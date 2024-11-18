using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.DTOs;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public List<Book>? Books { get; set; }

    public List<TradeRequest>? TradeRequests { get; set; }
}
