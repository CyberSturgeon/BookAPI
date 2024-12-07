using Books.DAL.DTOs;

namespace Books.BLL.Models;

public class BookFullModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public ICollection<TradeModel> TradeRequests { get; set; } = [];

    public ICollection<UserModel> Users { get; set; } = [];
}
