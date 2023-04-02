using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TClasesPermiso
    {
        public TClasesPermiso()
        {
            TUPermisos = new HashSet<TUPermiso>();
        }

        public int IdClase { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }

        public virtual ICollection<TUPermiso> TUPermisos { get; set; }
    }
}
