using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class DespachosConsolidadosDTO
    {
        public string Id_Producto { get; set; }
        public string Producto { get; set; }
        public double Volumen_Ordenado { get; set; }
        public double Volumen_Cargado { get; set; }
        public double Diferencia { get; set; }

    }
}
