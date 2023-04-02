using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TURole
    {
        public TURole()
        {
            TURolesPermisos = new HashSet<TURolesPermiso>();
            TUUsuarios = new HashSet<TUUsuario>();
        }

        public string IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TURolesPermiso> TURolesPermisos { get; set; }
        public virtual ICollection<TUUsuario> TUUsuarios { get; set; }
    }
}
