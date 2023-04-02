using System.Collections.Generic;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class RecetaAsignadaTerminalDTO
    {
        public string NombreReceta { get; set; }
        public bool Asignada { get; set; }
        public List<VigenciaRecetaDTO> Vigencias { get; set; }
        public List<RecetaComponenteDTO> Componentes { get; set; }
    }
}