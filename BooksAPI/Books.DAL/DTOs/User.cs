namespace Books.DAL.DTOs;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public IEnumerable<Book>? Books { get; set; }

    public IEnumerable<TradeRequest> Trades { get; set; }
}
