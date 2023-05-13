using author_service.Application;
using author_service.Handlers;
using author_service.Persistence;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using rabbitmq_bus.Bus;
using rabbitmq_bus.Implements;
using rabbitmq_bus.Queues;
using static author_service.Application.New;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

builder.Services.AddMediatR(typeof(Handler).Assembly);
builder.Services.AddAutoMapper(typeof(Handler).Assembly);

builder.Services.AddTransient<IBusEvent, BusEvent>();
builder.Services.AddTransient<IHandlerEvent<EmailEventQueue>, EmailEventHandler>();

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

app.Services.GetRequiredService<IBusEvent>().Subscribe<EmailEventQueue, EmailEventHandler>();

app.Run();
