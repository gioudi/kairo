using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class DatosConsultaPeticion
    {
        public int IdNumEntidad { get; set; }
        public string IdEntidad { get; set; }
        public DateTime Fecha { get; set; }
        public bool Lectura { get; set; }
    }
}
