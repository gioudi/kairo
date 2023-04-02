using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class TerminalCompañiaDTO
    {
        public string IdEntidad { get; set; }
        public string Nombre { get; set; }
        public bool EsTerminal { get; set; }
        public string IdEntidadPadre { get; set; }
        public bool Habilitada { get; set; }
        public List<TerminalCompañiaDTO> Compañias { get; set; }
    }
}
