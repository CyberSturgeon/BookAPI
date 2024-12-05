using Books.DAL.Repositories.Interfaces;
using Books.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books.DAL;

public static class ServicesConfiguration
{
    public static void AddDalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BooksContext>(options => options.UseNpgsql(configuration.GetConnectionString("BooksDbConnection")));
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
    }
}
