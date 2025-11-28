namespace DeliveryHub.Pedidos.Api.Models
{
    public enum StatusPedido // deixa o status tipado, sem usar string solta.
    {
        Criado = 0,
        EmPreparacao = 1,
        EmRota = 2,
        Entregue = 3,
        Cancelado = 4
    }

    public class Pedido
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // bom para microserviços e evita conflitos de identidade. 
        public string ClienteNome { get; set; } = string.Empty;
        public string EnderecoEntrega { get; set; } = string.Empty;
        public StatusPedido Status { get; set; } = StatusPedido.Criado;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow; // usar UTC para evitar problemas de fuso horário.
        public decimal ValorTotal { get; internal set; }
    }
}