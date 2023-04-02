using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class DatosTanquePeticion
    {
        public string Tanque { get; set; }
        public string IdTerminal { get; set; }
        public bool Lectura { get; set; }
    }
}
