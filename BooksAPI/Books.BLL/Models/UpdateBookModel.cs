
using Books.DAL.DTOs;

namespace Books.BLL.Models;

public class UpdateBookModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public ICollection<TradeRequest>? TradeRequests { get; set; }

    public ICollection<UserModel>? Users { get; set; }
}
