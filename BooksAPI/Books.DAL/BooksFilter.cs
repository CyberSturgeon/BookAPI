
namespace Books.DAL;

public class BooksFilter
{
    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public BooksFilter(string name, string author, string genre)
    {
        Name = name;
        Author = author;
        Genre = genre;
    }
}
