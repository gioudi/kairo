using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities

{
    public partial class TProcesamientoArchivosDet
    {
        public string IdColumna { get; set; }
        public string IdMapeo { get; set; }
        public int IndiceColumna { get; set; }
        public string IdTabla { get; set; }
        public string IdCampo { get; set; }
        public int Prioridad { get; set; }
        public bool Habilitado { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TProcesamientoArchivosMst IdMapeoNavigation { get; set; }

    }
}
