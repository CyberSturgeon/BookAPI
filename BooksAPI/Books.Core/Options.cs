
namespace Books.Core;

public class Options
{
    public const string ConnectionString = "Host=localhost;Port=5432;Database=Books;Username=postgres;Password=postgres;";

    public static string Issuer
    {
        get
        {
            return "KolyaService";
        }
    }

    public static string Audience
    {
        get
        {
            return "KolyaServiceUsers";
        }
    }

    public static string Key
    {
        get
        {
            return "abobaLupaPupa@abobaLupaPupa@abobaLupaPupa@";
        }
    }
}
