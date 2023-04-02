using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class PermisosDTO
    {   public int IdPermiso { get; set; }
        public string Nombre { get; set; }
        public int? IdEntidadPadre { get; set; }
        public bool Habilitada { get; set; }
        public int IdClase { get; set; }
        public List<PermisosDTO> Permisos { get; set; }
    }
}
