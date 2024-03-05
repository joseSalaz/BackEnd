using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseResponse
{
    public class UsuarioResponse
    {
        public int IdUsuario { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Cargo { get; set; }

        public bool? Estado { get; set; }

        public int IdPersona { get; set; }
        //public virtual ICollection<CajaResponse> CajaIdUsuarioAperturaNavigations { get; set; } = new List<Caja>();
        //public virtual ICollection<CajaCajaResponse> CajaIdUsuarioCierreNavigations { get; set; } = new List<Caja>();
        //public virtual PersonaCajaResponse? IdPersonaNavigation { get; set; }
        //public virtual ICollection<PedidoCajaResponse> Pedidos { get; set; } = new List<Pedido>();
    }
}
