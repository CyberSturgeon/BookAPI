
using Books.DAL.DTOs;

namespace Books.BLL.Models;

public class UpdateUserModel
{
    public string Name { get; set; }

    public string Email { get; set; }

    public ICollection<Book>? Books { get; set; }

    public ICollection<TradeRequest> Trades { get; set; }
}
