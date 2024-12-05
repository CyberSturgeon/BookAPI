
namespace Books.DAL.DTOs;

public class Book
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public ICollection<TradeRequest> TradeRequests { get; set; } = [];

    public ICollection<User> Users { get; set; } = [];
}
