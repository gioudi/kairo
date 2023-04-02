using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class MenuViewModel
    {
        public int IdPermiso { get; set; }
        public string Icono { get; set; }
        public string Nombre { get; set; }
        public string Accion { get; set; }
        public string Controlador { get; set; }
        public int? IdPermisoPadre { get; set; }
        public string Clase { get; set; }
        public string Color { get; set; }
        public List<MenuViewModel> SubMenus { get; set; }
    }
}
