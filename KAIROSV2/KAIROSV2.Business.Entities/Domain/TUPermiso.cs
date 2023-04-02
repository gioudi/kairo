using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TUPermiso
    {
        public TUPermiso()
        {
            InverseIdPermisoPadreNavigation = new HashSet<TUPermiso>();
            TURolesPermisos = new HashSet<TURolesPermiso>();
        }

        public int IdPermiso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Color { get; set; }
        public int? IdPermisoPadre { get; set; }
        public string Contenido { get; set; }
        public int IdClase { get; set; }

        public virtual TClasesPermiso IdClaseNavigation { get; set; }
        public virtual TUPermiso IdPermisoPadreNavigation { get; set; }
        public virtual ICollection<TUPermiso> InverseIdPermisoPadreNavigation { get; set; }
        public virtual ICollection<TURolesPermiso> TURolesPermisos { get; set; }
    }
}
