using DeliveryHub.Pedidos.Api.Dtos;
using DeliveryHub.Pedidos.Api.Models;
using DeliveryHub.Pedidos.Api.Repositories;

namespace DeliveryHub.Pedidos.Api.Services
{
    public class PedidoService : IPedidoService 
    {
        private readonly IPedidoRepository _repository; // Injeção de dependência do repositório
        public PedidoService(IPedidoRepository repository) // Construtor
        {
            _repository = repository; // Inicializa o repositório
        }

        public async Task<PedidoResponse> CriarAsync(CriarPedidoRequest request) // Método para criar um novo pedido
        {
            // Regra simples: valor não pode ser negativo
            if (request.ValorTotal <= 0) // Validação do valor total
            {
                throw new ArgumentException("Valor total do pedido deve ser maior que zero."); // Lança exceção se inválido
            }

            var pedido = new Pedido // Cria nova instância de Pedido
            {
                ClienteNome = request.ClienteNome,
                EnderecoEntrega = request.EnderecoEntrega,
                ValorTotal = request.ValorTotal,
                Status = StatusPedido.Criado
            };

            await _repository.AdicionarAsync(pedido); // Adiciona o pedido ao repositório

            return MapToResponse(pedido); // Mapeia e retorna o pedido criado
        }

        public async Task<IReadOnlyList<PedidoResponse>> ListarAsync() // Método para listar todos os pedidos
        {
            var pedidos = await _repository.ListarAsync(); // Obtém a lista de pedidos do repositório
            return pedidos.Select(MapToResponse).ToList(); // Mapeia e retorna a lista de pedidos
        }

        public async Task<PedidoResponse?> ObterPorIdAsync(Guid id) // Método para obter um pedido por ID
        {
            var pedido = await _repository.ObterPorIdAsync(id); // Obtém o pedido do repositório
            return pedido is null ? null : MapToResponse(pedido); // Mapeia e retorna o pedido ou null se não encontrado
        }

        public async Task<bool> AtualizarStatusAsync(Guid id, AtualizarStatusPedidoRequest request) // Método para atualizar o status do pedido
        {
            var pedido = await _repository.ObterPorIdAsync(id); // Obtém o pedido do repositório
            if (pedido is null) return false; // Retorna falso se o pedido não for encontrado

            if (!Enum.TryParse<StatusPedido>(request.NovoStatus, ignoreCase: true, out var novoStatus)) // Tenta converter o status informado
            {
                throw new ArgumentException("Status informado é inválido."); // Lança exceção se o status for inválido
            }

            pedido.Status = novoStatus; // Atualiza o status do pedido
            await _repository.AtualizarAsync(pedido); // Salva a atualização no repositório
            return true; // Retorna verdadeiro indicando sucesso
        }

        private static PedidoResponse MapToResponse(Pedido pedido) // Método auxiliar para mapear Pedido para PedidoResponse
        {
            return new PedidoResponse // Cria nova instância de PedidoResponse
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