namespace BooksAPI.Models.Responses;

public class UserFullResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public ICollection<BookShortResponse> Books { get; set; }

    public ICollection<TradeRequestResponse> TradeRequests { get; set; }
}
