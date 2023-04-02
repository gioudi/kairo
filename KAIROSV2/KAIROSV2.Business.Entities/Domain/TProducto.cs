using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TProducto
    {
        public TProducto()
        {
            TTerminalContador = new HashSet<TTerminalContador>();
            TLineas = new HashSet<TLinea>();
            TProductosAtributos = new HashSet<TProductosAtributo>();
            TProductosReceta = new HashSet<TProductosReceta>();
            TProductosRecetasComponentes = new HashSet<TProductosRecetasComponente>();
            TRecibosBases = new HashSet<TRecibosBase>();
            TRecibosContadorMezclados = new HashSet<TRecibosContadorMezclado>();
            TRecibosContador = new HashSet<TRecibosContador>();
            TTanques = new HashSet<TTanque>();
            TTerminalCompañiasProductos = new HashSet<TTerminalCompañiasProducto>();
        }

        public string IdProducto { get; set; }
        public string NombreErp { get; set; }
        public string NombreCorto { get; set; }
        public string Sicom { get; set; }
        public string IdClase { get; set; }
        public string IdTipo { get; set; }
        public string Estado { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TProductosClase IdClaseNavigation { get; set; }
        public virtual TProductosTipo IdTipoNavigation { get; set; }
        public virtual ICollection<TTerminalContador> TTerminalContador { get; set; }        
        public virtual ICollection<TLinea> TLineas { get; set; }
        public virtual ICollection<TProductosAtributo> TProductosAtributos { get; set; }
        public virtual ICollection<TProductosReceta> TProductosReceta { get; set; }
        public virtual ICollection<TProductosRecetasComponente> TProductosRecetasComponentes { get; set; }
        public virtual ICollection<TRecibosBase> TRecibosBases { get; set; }
        public virtual ICollection<TRecibosContadorMezclado> TRecibosContadorMezclados { get; set; }
        public virtual ICollection<TRecibosContador> TRecibosContador { get; set; }
        public virtual ICollection<TTanque> TTanques { get; set; }
        public virtual ICollection<TTerminalCompañiasProducto> TTerminalCompañiasProductos { get; set; }
    }
}
