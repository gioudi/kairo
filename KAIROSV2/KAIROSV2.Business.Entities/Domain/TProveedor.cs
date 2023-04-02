using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TProveedor
    {
        public TProveedor()
        {
            TProveedoresPlanta = new HashSet<TProveedoresPlanta>();
            TProveedoresProductos = new HashSet<TProveedoresProducto>();
            TRecibosFacturacions = new HashSet<TRecibosFacturacion>();
            TRecibosTransportes = new HashSet<TRecibosTransporte>();
        }

        public string IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string SicomProveedor { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TProveedoresPlanta> TProveedoresPlanta { get; set; }
        public virtual ICollection<TProveedoresProducto> TProveedoresProductos { get; set; }
        public virtual ICollection<TRecibosFacturacion> TRecibosFacturacions { get; set; }
        public virtual ICollection<TRecibosTransporte> TRecibosTransportes { get; set; }
    }
}
