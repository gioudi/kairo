using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class ProductoTerminalDto
    {
        public bool Asignado { get; set; }
        public string Icon { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreCorto { get; set; }
        public string NombreTerminal { get; set; }
        public List<RecetaProductoTerminalDto> Recetas { get; set; }
    }
}
