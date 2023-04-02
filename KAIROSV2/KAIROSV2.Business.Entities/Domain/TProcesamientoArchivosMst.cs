using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities

{
    public partial class TProcesamientoArchivosMst
    {
        public TProcesamientoArchivosMst()
        {
            ProcesamientoArchivosDets = new HashSet<TProcesamientoArchivosDet>();
        }

        public string IdMapeo { get; set; }
        public string Descripcion { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public char Separador { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TProcesamientoArchivosDet> ProcesamientoArchivosDets { get; set; }

    }
}
