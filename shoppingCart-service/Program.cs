using shoppingCart_service.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using static shoppingCart_service.Application.New;
using FluentValidation.AspNetCore;
using shoppingCart_service.Application;
using shoppingCart_service.RemoteInterface;
using shoppingCart_service.RemoteService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CartContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"), new MySqlServerVersion(new Version(8, 0, 33))));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddMediatR(typeof(Handler).Assembly);
builder.Services.AddHttpClient("Books", config => config.BaseAddress = new Uri(builder.Configuration["Services:Books"]));

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<New>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
