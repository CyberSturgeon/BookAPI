
namespace Books.Core;

public class Options
{
    public static string ConnectionString
    {
        get
        {
            return Environment.GetEnvironmentVariable("BooksDB");
        }
    }
}
