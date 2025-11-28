using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DeliveryHub.Entregas.Api.Services;

namespace DeliveryHub.Entregas.Api.Messaging
{
    public class PedidoCriadoEvent 
    {
        public Guid PedidoId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
    }

    public class PedidoCriadoConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;

        public PedidoCriadoConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(
                exchange: "deliveryhub-pedidos-exchange",
                type: ExchangeType.Topic,
                durable: true
            );

            _channel.QueueDeclare(
                queue: "entregas-pedido-criado",
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            _channel.QueueBind(
                queue: "entregas-pedido-criado",
                exchange: "deliveryhub-pedidos-exchange",
                routingKey: "pedido.criado"
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var evento = JsonSerializer.Deserialize<PedidoCriadoEvent>(json);

                if (evento != null)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var entregaService = scope.ServiceProvider.GetRequiredService<IEntregaService>();

                        await entregaService.CriarEntregaPorEventoAsync(
                            evento.PedidoId,
                            evento.ClienteNome,
                            evento.ValorTotal
                        );
                    }
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(
                queue: "entregas-pedido-criado",
                autoAck: false,
                consumer: consumer
            );

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose(); // 
        }
    }
}
