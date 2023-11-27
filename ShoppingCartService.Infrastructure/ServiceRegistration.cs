using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShoppingCartService.Application.Interfaces.Repositories;
using ShoppingCartService.Infrastructure.Context;
using ShoppingCartService.Infrastructure.Repositories;

namespace ShoppingCartService.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddServiceCollection (this IServiceCollection services)
    {
        services.AddScoped<IShoppingCartRepository, ShoppingCartReposityory>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddDbContext<DataContext>(options => 
            options.UseSqlServer("Server=localhost;DataBase=Trendyol;Trusted_Connection=false;User Id=username; Password=password;TrustServerCertificate=true")
            );

        return services;
    }
}