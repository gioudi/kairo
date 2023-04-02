using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class TanqueDetallesDTO
    {
        public string Tanque { get; set; }
        public string Terminal { get; set; }

        //Detalles
        public string TipoTanque { get; set; }
        public int CapacidadNominal { get; set; }
        public int CapacidadOperativa { get; set; }

        public double VolumenNoBombeable { get; set; }
        public int AlturaMaximaAforo { get; set; }
        public string AforadoPor { get; set; }
        public DateTime FechaAforo { get; set; }
        public string Observaciones { get; set; }

        public bool PantallaFlotante { get; set; }
        public double DensidadAforo { get; set; }
        public double GalonesPorGrado { get; set; }
        public int NivelCorreccionInicial { get; set; }
        public int NivelCorreccionFinal { get; set; }


    }
}
