using Microsoft.EntityFrameworkCore;
using ThunderWingsECommerceChallenge;
using ThunderWingsECommerceChallenge.Api.HealthChecks;
using ThunderWingsECommerceChallenge.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

//add health checks
builder.Services.AddHealthChecks()
    .AddCheck<SimpleHealthCheck>("SimpleHealthCheck");

//add mediatr
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add DbContext with in-memory database
builder.Services.AddDbContext<ThunderWingsDatabaseContext>(options =>
    options.UseInMemoryDatabase(databaseName: "ThunderWingsECommerceDatabase"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Seed the data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Get Db Context
    var context = services.GetRequiredService<ThunderWingsDatabaseContext>();

    // Check if the database needs to be created
    context.Database.EnsureCreated();

    // Seed the data 
    if (!context.Aircraft.Any())
    {
        SeedData.Initialize(context);
    }
}


// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
