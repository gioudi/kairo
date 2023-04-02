using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTerminal
    {
        public TTerminal()
        {
            TTerminalContador = new HashSet<TTerminalContador>();
            TLineas = new HashSet<TLinea>();
            TRecibosBases = new HashSet<TRecibosBase>();
            TTanques = new HashSet<TTanque>();
            TTerminalCompañia = new HashSet<TTerminalCompañia>();
            TDespacho = new HashSet<TDespacho>();
            TTASCortes = new HashSet<TTASCortes>();
        }


        public string IdTerminal { get; set; }
        public string Terminal { get; set; }
        public int IdEstado { get; set; }
        public string IdArea { get; set; }
        public string Superintendente { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CentroCosto { get; set; }
        public bool Conjunta { get; set; }
        public bool VentasTerceros { get; set; }
        public string TipoInformeTerceros { get; set; }
        public string Poliducto { get; set; }
        public string IdCompañiaOperadora { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }

        public int FilaId { get; set; }

        public virtual TArea IdAreaNavigation { get; set; }
        public virtual TTerminalesEstado IdEstadoNavigation { get; set; }
        public virtual ICollection<TTerminalContador> TTerminalContador { get; set; }
        public virtual ICollection<TLinea> TLineas { get; set; }
        public virtual ICollection<TRecibosBase> TRecibosBases { get; set; }
        public virtual ICollection<TTanque> TTanques { get; set; }
        public virtual ICollection<TTerminalCompañia> TTerminalCompañia { get; set; }
        public virtual ICollection<TTerminalesProductosReceta> TTerminalesProductosReceta { get; set; }
        public virtual ICollection<TDespacho> TDespacho { get; set; }
        public virtual ICollection<TTASCortes> TTASCortes { get; set; }
    }
}
