
namespace Books.BLL.Models;

public class CreateBookModel
{
    public string Name { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public Guid UserId { get; set; }
}
