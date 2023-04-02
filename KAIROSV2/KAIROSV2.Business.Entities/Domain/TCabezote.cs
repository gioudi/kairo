using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TCabezote
    {
        public string PlacaCabezote { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }
    }
}
