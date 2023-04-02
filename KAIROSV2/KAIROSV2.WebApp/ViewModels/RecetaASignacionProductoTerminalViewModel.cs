using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class RecetaAsignacionProductoTerminal
    {
        public IEnumerable<TArea> Areas { get; set; }
        public IEnumerable<string> Encabezados { get; set; }

        public string NombreReceta { get; set; }

        public bool Asignada { get; set; }

        //public List<vigencia> Vigencia { get; set; }

        //public List<ComponentesReceta> Componentes { get; set; }

    }
}