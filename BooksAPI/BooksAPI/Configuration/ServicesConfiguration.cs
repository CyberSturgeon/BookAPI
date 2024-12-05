using BooksAPI.Mappings;
using BooksAPI.Models.Requests.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BooksAPI.Configuration;

public static class ServicesConfiguration
{
    public static void AddApiServices (this IServiceCollection services)
    {
        services.AddAuth();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateBookRequestValidator>();
        services.AddAutoMapper(typeof(BookMapperProfile).Assembly,
                typeof(UserMapperProfile).Assembly,
                typeof(TradeMapperProfile).Assembly);
    }
}