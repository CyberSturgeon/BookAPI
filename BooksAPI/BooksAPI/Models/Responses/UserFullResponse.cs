namespace BooksAPI.Models.Responses;

public class UserFullResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public List<BookShortResponse> Books { get; set; }

    public List<TradeRequestResponse> TradeRequests { get; set; }
}
