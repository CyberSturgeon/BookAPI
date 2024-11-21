
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

    public static string Issuer
    {
        get
        {
            return Environment.GetEnvironmentVariable("BooksIssuer");
        }
    }

    public static string Audience
    {
        get
        {
            return Environment.GetEnvironmentVariable("BooksAudience");
        }
    }

    public static string Key
    {
        get
        {
            return Environment.GetEnvironmentVariable("BooksKey");
        }
    }
}
