namespace BooksAPI.Models.Responses;

public class BookFullResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public ICollection<UserResponse> Users { get; set; }

    public ICollection<TradeRequestResponse> TradeRequests { get; set; }
}
