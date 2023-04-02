using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TCompañiasTransportadora
    {
        public TCompañiasTransportadora()
        {
            TRecibosTransportes = new HashSet<TRecibosTransporte>();
        }

        public string IdTransportadora { get; set; }
        public string NombreTransportadora { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TRecibosTransporte> TRecibosTransportes { get; set; }
    }
}
