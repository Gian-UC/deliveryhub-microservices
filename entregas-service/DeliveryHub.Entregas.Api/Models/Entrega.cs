namespace DeliveryHub.Entregas.Api.Models
{
    public class Entrega
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // bom para microserviços.
        public Guid PedidoId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;

        // Usa o enum em vez de string solta
        public StatusEntrega Status { get; set; } = StatusEntrega.EmRota;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow; // sempre em UTC
    }
}
