using DeliveryHub.Entregas.Api.Data;
using DeliveryHub.Entregas.Api.Repositories;
using DeliveryHub.Entregas.Api.Services;
using DeliveryHub.Entregas.Api.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext (PostgreSQL)
builder.Services.AddDbContext<EntregasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
