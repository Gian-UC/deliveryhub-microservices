using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DeliveryHub.Pedidos.Api.Events;

namespace DeliveryHub.Pedidos.Api.Messaging
{
    public class PedidoCriadoProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public PedidoCriadoProducer()
        {
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
        }

        public void PublicarPedidoCriado(PedidoCriadoEvent evento)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evento));

            _channel.BasicPublish(
                exchange: "deliveryhub-pedidos-exchange",
                routingKey: "pedido.criado",
                basicProperties: null,
                body: body
            );
        }
        
    }
}