namespace SampleBackend.Models.Requests;

public class CreateBookRequest
{
    public string Name { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public Guid UserId { get; set; }
}
