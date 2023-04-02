using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class RecetaProductoTerminalDto
    {
        public string NombreReceta { get; set; }
        public bool Asignada { get; set; }
        public List<VigenciaRecetaDTO> Vigencias { get; set; }
        public List<RecetaComponenteDTO> Componentes { get; set; }
    }
}
