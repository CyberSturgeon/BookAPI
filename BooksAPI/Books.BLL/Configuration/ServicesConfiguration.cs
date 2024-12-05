using Books.BLL.Services.Interfaces;
using Books.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using Books.BLL.Mappings;

namespace Books.BLL.Configuration;

public static class ServicesConfiguration
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddScoped<IBooksService, BooksService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddAutoMapper(typeof(BookMapperProfile).Assembly);
    }
}
