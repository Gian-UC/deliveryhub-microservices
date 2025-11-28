using DeliveryHub.Entregas.Api.Repositories;
using DeliveryHub.Entregas.Api.Services;
using DeliveryHub.Entregas.Api.Messaging;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositório em memória
builder.Services.AddSingleton<IEntregaRepository, InMemoryEntregaRepository>();

// Service
builder.Services.AddScoped<IEntregaService, EntregaService>();

// Consumer do RabbitMQ
builder.Services.AddHostedService<PedidoCriadoConsumer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
