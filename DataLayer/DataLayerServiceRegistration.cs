using DataLayer.Abstract;
using DataLayer.Concrete;
using DataLayer.Context;
using Domain.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer;

public static class DataLayerServiceRegistration
{
    public static void AddDataLayerRegistration(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseInMemoryDatabase("InMemoryDb");
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
    }
}
