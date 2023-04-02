using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class TerminalViewModel
    {
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

    }
}
