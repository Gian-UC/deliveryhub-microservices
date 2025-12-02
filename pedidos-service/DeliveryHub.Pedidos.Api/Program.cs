using DeliveryHub.Pedidos.Api.Messaging;
using DeliveryHub.Pedidos.Api.Repositories;
using DeliveryHub.Pedidos.Api.Services;
using DeliveryHub.Pedidos.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ Publisher
builder.Services.AddSingleton<PedidoCriadoProducer>();

// PostgreSQL
builder.Services.AddDbContext<PedidosDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PedidosDb")));

// Dependências
builder.Services.AddScoped<IPedidoRepository, EfPedidoRepository>();  
builder.Services.AddScoped<IPedidoService, PedidoService>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
