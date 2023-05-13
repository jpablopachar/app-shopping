using AutoMapper;
using book_service.Application;
using book_service.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using rabbitmq_bus.Bus;
using rabbitmq_bus.Implements;
using static book_service.Application.New;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

builder.Services.AddMediatR(typeof(Handler).Assembly);
builder.Services.AddAutoMapper(typeof(Handler).Assembly);

// builder.Services.AddTransient<IBusEvent, BusEvent>();
builder.Services.AddSingleton<IBusEvent, BusEvent>(subscription =>
{
    var scopeFactory = subscription.GetRequiredService<IServiceScopeFactory>();

    return new BusEvent(subscription.GetService<IMediator>(), scopeFactory);
});

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
