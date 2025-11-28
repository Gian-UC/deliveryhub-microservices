using DeliveryHub.Pedidos.Api.Dtos;
using DeliveryHub.Pedidos.Api.Events;
using DeliveryHub.Pedidos.Api.Messaging;
using DeliveryHub.Pedidos.Api.Models;
using DeliveryHub.Pedidos.Api.Repositories;

namespace DeliveryHub.Pedidos.Api.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly PedidoCriadoProducer _publisher;

        // 💙 CORREÇÃO — um construtor só, recebendo tudo
        public PedidoService(IPedidoRepository repository, PedidoCriadoProducer publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<PedidoResponse> CriarAsync(CriarPedidoRequest request)
        {
            if (request.ValorTotal <= 0)
                throw new ArgumentException("Valor total do pedido deve ser maior que zero.");

            var pedido = new Pedido
            {
                ClienteNome = request.ClienteNome,
                EnderecoEntrega = request.EnderecoEntrega,
                ValorTotal = request.ValorTotal,
                Status = StatusPedido.Criado
            };

            await _repository.AdicionarAsync(pedido);

            // 🔵 Publicar evento no RabbitMQ
            var evento = new PedidoCriadoEvent
            {
                PedidoId = pedido.Id,
                ClienteNome = pedido.ClienteNome,
                ValorTotal = pedido.ValorTotal
            };

            _publisher.PublicarPedidoCriado(evento);

            return MapToResponse(pedido);
        }

        public async Task<IReadOnlyList<PedidoResponse>> ListarAsync()
        {
            var pedidos = await _repository.ListarAsync();
            return pedidos.Select(MapToResponse).ToList();
        }

        public async Task<PedidoResponse?> ObterPorIdAsync(Guid id)
        {
            var pedido = await _repository.ObterPorIdAsync(id);
            return pedido is null ? null : MapToResponse(pedido);
        }

        public async Task<bool> AtualizarStatusAsync(Guid id, AtualizarStatusPedidoRequest request)
        {
            var pedido = await _repository.ObterPorIdAsync(id);
            if (pedido is null) return false;

            if (!Enum.TryParse<StatusPedido>(request.NovoStatus, ignoreCase: true, out var novoStatus))
                throw new ArgumentException("Status informado é inválido.");

            pedido.Status = novoStatus;
            await _repository.AtualizarAsync(pedido);
            return true;
        }

        private static PedidoResponse MapToResponse(Pedido pedido)
        {
            return new PedidoResponse
            {
                Id = pedido.Id,
                ClienteNome = pedido.ClienteNome,
                EnderecoEntrega = pedido.EnderecoEntrega,
                ValorTotal = pedido.ValorTotal,
                Status = pedido.Status.ToString(),
                CriadoEm = pedido.CriadoEm
            };
        }
    }
}
