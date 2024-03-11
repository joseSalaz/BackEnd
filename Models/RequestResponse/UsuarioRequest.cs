using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestRequest
{
    public class UsuarioRequest
    {
        public int IdUsuario { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Cargo { get; set; }

        public bool? Estado { get; set; }

        public int IdPersona { get; set; }
        //public virtual ICollection<CajaRequest> CajaIdUsuarioAperturaNavigations { get; set; } = new List<Caja>();
        //public virtual ICollection<CajaCajaRequest> CajaIdUsuarioCierreNavigations { get; set; } = new List<Caja>();
        //public virtual PersonaCajaRequest? IdPersonaNavigation { get; set; }
        //public virtual ICollection<PedidoCajaRequest> Pedidos { get; set; } = new List<Pedido>();

    }
}
