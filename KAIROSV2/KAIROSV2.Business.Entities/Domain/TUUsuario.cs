using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TUUsuario
    {
        public TUUsuario()
        {
            TUUsuariosTerminalCompañia = new HashSet<TUUsuariosTerminalCompañia>();
        }

        public string IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string RolId { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TURole Rol { get; set; }

        public virtual TUUsuariosImagen TUUsuarioImagen { get; set; }        
        public virtual ICollection<TUUsuariosTerminalCompañia> TUUsuariosTerminalCompañia { get; set; }
    }
}
