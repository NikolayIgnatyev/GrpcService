using GrpcServiceServer.Services;
using Microsoft.EntityFrameworkCore;
using Server;
using Server.DataBase;
using Server.Encoding;
using Server.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ICipher, CaesarCipher>();
// получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<ICacher, Cacher>();
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<EncoderService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
