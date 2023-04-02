using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities.DTOs
{
   public class LineaDTO
   {
        public string Idlinea { get; set; }
        public string Terminal { get; set; }
        public string IdTerminal { get; set; }
        public string Producto { get; set; }
        public string Estado { get; set; }
        public string EstadoColor { get; set; }
        public int Capacidad { get; set; }
        public double DensidadAforo { get; set; }
        public string Observaciones { get; set; }
    }
}
