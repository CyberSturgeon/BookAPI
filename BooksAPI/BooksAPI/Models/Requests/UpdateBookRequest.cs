namespace BooksAPI.Models.Requests;

public class UpdateBookRequest
{
    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }
}
