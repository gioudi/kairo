using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TCompañia
    {
        public TCompañia()
        {
            TRecibosContadors = new HashSet<TRecibosContador>();
            TRecibosFacturacions = new HashSet<TRecibosFacturacion>();
            TTerminalCompañia = new HashSet<TTerminalCompañia>();
            TTerminalCompañiasProductos = new HashSet<TTerminalCompañiasProducto>();
        }

        public string IdCompañia { get; set; }
        public string Nombre { get; set; }
        public string SalesOrganization { get; set; }
        public string DistributionChannel { get; set; }
        public string Division { get; set; }
        public string SupplierType { get; set; }
        public string CodigoSicom { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TRecibosContador> TRecibosContadors { get; set; }
        public virtual ICollection<TRecibosFacturacion> TRecibosFacturacions { get; set; }
        public virtual ICollection<TTerminalCompañia> TTerminalCompañia { get; set; }
        public virtual ICollection<TTerminalCompañiasProducto> TTerminalCompañiasProductos { get; set; }
    }
}
