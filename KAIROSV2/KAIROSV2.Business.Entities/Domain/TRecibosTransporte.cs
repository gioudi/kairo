using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosTransporte
    {
        public long IdRecibo { get; set; }
        public string IdProveedor { get; set; }
        public string PlacaCabezote { get; set; }
        public string PlacaTrailer { get; set; }
        public string NoGuia { get; set; }
        public DateTime FechaGuia { get; set; }
        public string IdTransportadora { get; set; }
        public string CedulaConductor { get; set; }
        public string CodigoAutorizacionSicom { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TProveedor IdProveedorNavigation { get; set; }
        public virtual TRecibosBase IdReciboNavigation { get; set; }
        public virtual TCompañiasTransportadora IdTransportadoraNavigation { get; set; }
    }
}
