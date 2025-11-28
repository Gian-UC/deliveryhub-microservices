using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryHub.Pedidos.Api.Events
{
    public class PedidoCriadoEvent
    {
        public Guid PedidoId { get; set; }
        public String ClienteNome { get; set ;} = string.Empty;
        public decimal ValorTotal { get; set; }
    }
}