using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryHub.Entregas.Api.Models
{
    public class Entrega
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // bom para microserviços e evita conflitos de identidade.
        public Guid PedidoId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public decimal ValorTotal { get; internal set; }
        public string Status { get; set; } = "Pendente";
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow; // usar UTC para evitar problemas de fuso horário.
    }
}