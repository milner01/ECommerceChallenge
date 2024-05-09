using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge;
using ThunderWingsECommerceChallenge.Models;
using ThunderWingsECommerceChallenge.Services.AircraftService;
using ThunderWingsECommerceChallenge.Services.Checkout;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

// Add DbContext with in-memory database
builder.Services.AddDbContext<ECommerceContext>(options =>
    options.UseInMemoryDatabase(databaseName: "ThunderWingsECommerceDatabase"));

// Add services
builder.Services.AddScoped<IAircraftService, AircraftService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();


var app = builder.Build();

// Seed the data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Get Db Context
    var context = services.GetRequiredService<ECommerceContext>();

    // Check if the database needs to be created
    context.Database.EnsureCreated();

    // Seed the data 
    if (!context.Aircraft.Any())
    {
        SeedData.Initialize(context);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();