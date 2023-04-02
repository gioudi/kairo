using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TProductosAtributo
    {
        public int IdAtributo { get; set; }
        public string IdProducto { get; set; }
        public string ValorTexto { get; set; }
        public double? ValorNumero { get; set; }
        public string Observaciones { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TAtributo IdAtributoNavigation { get; set; }
        public virtual TProducto IdProductoNavigation { get; set; }
    }
}
