using Business;
using DataLayer;
using DataLayer.Context;
using Domain.Concrete.Entities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddBusinessRegistration();

builder.Services.AddDataLayerRegistration();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "Bu API, .NET 9 Core ile oluþturulmuþtur.",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = "";
    });
}

var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

// Eðer hiç kitap yoksa ekle
if (!dbContext.Books.Any())
{
    dbContext.Books.AddRange(
        new Book { Id = 100, Title = "Panik", Author = "Jeff Abbott", Stock = 10, Price = 200.5M },
        new Book { Id = 101, Title = "Ýntibah", Author = "Namýk Kemail", Stock = 20, Price = 500M },
        new Book { Id = 102, Title = "ATATÜRK", Author = "Ýlber Ortaylý", Stock = 1938, Price = 9999M },
        new Book { Id = 103, Title = "Ýstanbul Hatýrasý", Author = "Ahmet Ümit", Stock = 5, Price = 150M }
    );
    dbContext.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
