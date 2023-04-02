using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class DatosTerminalProductoPeticion
    {
        public string IdTerminal { get; set; }
        public string IdProducto { get; set; }
        public bool Lectura { get; set; }
    }
}
