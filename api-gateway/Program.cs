using api_gateway.MessageHandler;
using api_gateway.RemoteImplement;
using api_gateway.RemoteInterface;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers();
builder.Services.AddSingleton<IRemoteAuthor, RemoteAuthor>();
builder.Services.AddHttpClient("AuthorService", config => config.BaseAddress = new Uri(builder.Configuration["Services:Author"]));
builder.Services.AddOcelot().AddDelegatingHandler<BookHandler>();
builder.Configuration.AddJsonFile($"ocelot.json");
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
app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
