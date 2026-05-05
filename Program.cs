using Carter;
using Microsoft.EntityFrameworkCore;
using mini_crm.Infrastructure;
using mini_crm.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});


var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlite(connectionString);
});

builder.Services.AddScoped<IUtil, Util>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCarter();

app.UseHttpsRedirection();


app.Run();

