namespace SampleBackend.Models.Responses;

public class TradeResponse
{
    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    public Guid UserId { get; set; }

    //[DefaultValue(Status.Waiting)]
    public string TradeStatus { get; set; }
}
