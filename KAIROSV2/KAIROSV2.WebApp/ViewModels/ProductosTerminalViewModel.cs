using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class ProductosTerminalViewModel
    {

        public bool Asignado { get; set; }

        public string Icon { get; set; }

        public string CodigoProducto { get; set; }

        public string NombreCorto { get; set; }

        public List<RecetaTerminalViewModel> Recetas { get; set; }


    }
}