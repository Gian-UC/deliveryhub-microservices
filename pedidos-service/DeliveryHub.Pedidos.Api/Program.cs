using DeliveryHub.Pedidos.Api.Repositories;
using DeliveryHub.Pedidos.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI - Registrar dependências
builder.Services.AddSingleton<IPedidoRepository, InMemoryPedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection(); lembra: em Docker, estamos usando HTTP
app.MapControllers();

app.Run();
