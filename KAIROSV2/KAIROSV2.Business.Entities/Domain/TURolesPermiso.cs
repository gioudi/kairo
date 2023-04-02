using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TURolesPermiso
    {
        public string IdRol { get; set; }
        public int IdPermiso { get; set; }
        public bool Activo { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TUPermiso IdPermisoNavigation { get; set; }
        public virtual TURole IdRolNavigation { get; set; }
    }
}
