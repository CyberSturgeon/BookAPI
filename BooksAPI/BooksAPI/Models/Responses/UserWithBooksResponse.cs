namespace SampleBackend.Models.Responses;

public class UserWithBooksResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public int BooksCount { get; set; }

    public List<BookShortResponse> Books { get; set; }
}
