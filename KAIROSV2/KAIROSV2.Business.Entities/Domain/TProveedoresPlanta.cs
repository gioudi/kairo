using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TProveedoresPlanta
    {
        public string IdProveedor { get; set; }
        public string PlantaProveedor { get; set; }
        public string SicomPlantaProveedor { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TProveedor IdProveedorNavigation { get; set; }
    }
}
