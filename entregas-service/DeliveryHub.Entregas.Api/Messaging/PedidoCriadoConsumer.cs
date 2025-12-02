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
    }

    public class PedidoCriadoConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection? _connection;
        private IModel? _channel;

        public PedidoCriadoConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest",
                DispatchConsumersAsync = true
            };

            // Retry de conexão
            var retries = 10;

            while (retries > 0)
            {
                try
                {
                    Console.WriteLine("Tentando conectar ao RabbitMQ...");
                    _connection = factory.CreateConnection();
                    _channel = _connection.CreateModel();
                    Console.WriteLine("Conectado ao RabbitMQ com sucesso!");
                    break;
                }
                catch
                {
                    retries--;
                    Console.WriteLine($"RabbitMQ não está pronto. Tentando novamente... ({10 - retries}/10)");
                    Thread.Sleep(3000);
                }
            }

            if (_connection == null)
                throw new Exception("Não foi possível conectar ao RabbitMQ.");

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
            var consumer = new AsyncEventingBasicConsumer(_channel!);

            consumer.Received += async (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"[x] Evento recebido: {json}");

                var evento = JsonSerializer.Deserialize<PedidoCriadoEvent>(json);

                if (evento != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var entregaService = scope.ServiceProvider.GetRequiredService<IEntregaService>();

                    await entregaService.CriarEntregaAsync(
                        evento.PedidoId,
                        evento.ClienteNome
                    );

                    Console.WriteLine($"[✓] Entrega criada para o pedido {evento.PedidoId}");
                }

                _channel!.BasicAck(ea.DeliveryTag, false);
            };

            _channel!.BasicConsume(
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
            base.Dispose();
        }
    }
}
