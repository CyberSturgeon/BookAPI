
using Books.DAL.DTOs;

namespace Books.BLL.Models;

public class UserFullModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public ICollection<BookModel> Books { get; set; } = [];

    public ICollection<TradeModel> Trades { get; set; } = [];
}
