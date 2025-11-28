namespace DeliveryHub.Pedidos.Api.Dtos
{
    // DTO para criação de pedido
    public class CriarPedidoRequest
    {
        public string ClienteNome { get; set; } = string.Empty;
        public string EnderecoEntrega { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
    }

    // DTO para atualizar status
    public class AtualizarStatusPedidoRequest
    {
        public string NovoStatus { get; set; } = string.Empty;
    }

    // DTO de resposta
    public class PedidoResponse
    {
        public Guid Id { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public string EnderecoEntrega { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
    }
}
