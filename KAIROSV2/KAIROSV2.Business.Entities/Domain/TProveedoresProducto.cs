using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TProveedoresProducto
    {
        public string IdProveedor { get; set; }
        public string IdTipoProducto { get; set; }
        public int FilaId { get; set; }

        public virtual TProveedor IdProveedorNavigation { get; set; }
        public virtual TProductosTipo IdTipoProductoNavigation { get; set; }
    }
}
