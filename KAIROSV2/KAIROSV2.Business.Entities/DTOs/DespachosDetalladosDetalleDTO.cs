using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class DespachosDetalladosDetalleDTO
    {
        public string Componente { get; set; }
        public double Volumen_Bruto { get; set; }
        public double Volumen_Neto { get; set; }
        public double Temperatura { get; set; }
        public double Densidad { get; set; }


    }
}
