using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryHub.Entregas.Api.Dtos
{
    public class EntregaResponse
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
    }
}