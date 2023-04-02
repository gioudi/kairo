using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosContadorMezclado
    {
        public long IdRecibo { get; set; }
        public string IdProductoMezcla { get; set; }
        public string IdContadorMezcla { get; set; }
        public string IdProductoFinal { get; set; }
        public double ContadorMezclaInicial { get; set; }
        public double ContadorMezclaFinal { get; set; }
        public double VolumenContadorMezclaBruto { get; set; }
        public double VolumenContadorMezclaNeto { get; set; }
        public double DensidadMuestra { get; set; }
        public double TemperaturaPromedioMuestra { get; set; }
        public double FactorCorreccion { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TContador IdContadorMezclaNavigation { get; set; }
        public virtual TProducto IdProductoMezclaNavigation { get; set; }
        public virtual TRecibosContador IdReciboNavigation { get; set; }
    }
}
