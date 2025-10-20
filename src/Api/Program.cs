using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Setup Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup Fusion Memory Cache and Redis
// builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = "<RedisConnectionString>"; });
// builder.Services.AddMemoryCache();

// Setup Serilog
// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .WriteTo.LogstashTcpNetCore("<LogstashIPAddress>", 5000)
//     .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();