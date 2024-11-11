namespace SampleBackend.Models.Responses;

public class BookShortResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }
}
