using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TProductosClase
    {
        public TProductosClase()
        {
            TProductos = new HashSet<TProducto>();
        }

        public string IdClase { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public string Icono { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TProducto> TProductos { get; set; }
    }
}
