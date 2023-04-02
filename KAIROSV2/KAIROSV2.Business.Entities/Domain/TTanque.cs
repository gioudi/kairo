using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTanque
    {
        public TTanque()
        {
            TRecibosBases = new HashSet<TRecibosBase>();
            TTanquesAforoResponsables = new HashSet<TTanquesAforoResponsable>();
            TTanquesAforos = new HashSet<TTanquesAforo>();            
        }

        public string IdTanque { get; set; }
        public string IdTerminal { get; set; }
        public string IdProducto { get; set; }
        public string TipoTanque { get; set; }
        public string ClaseTanque { get; set; }
        public int IdEstado { get; set; }
        public int CapacidadNominal { get; set; }
        public int CapacidadOperativa { get; set; }
        public bool PantallaFlotante { get; set; }
        public double VolumenNoBombeable { get; set; }
        public int AlturaMaximaAforo { get; set; }        
        public string AforadoPor { get; set; }
        public DateTime FechaAforo { get; set; }
        public string Observaciones { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TTanquesEstado IdEstadoNavigation { get; set; }
        public virtual TProducto IdProductoNavigation { get; set; }
        public virtual TTerminal IdTerminalNavigation { get; set; }
        public virtual TTanquesPantallaFlotante IdTanquesPantallaFlotanteNavigation { get; set; }        
        public virtual ICollection<TRecibosBase> TRecibosBases { get; set; }
        public virtual ICollection<TTanquesAforoResponsable> TTanquesAforoResponsables { get; set; }
        public virtual ICollection<TTanquesAforo> TTanquesAforos { get; set; }
    }
}
