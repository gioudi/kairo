using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class DespachoConsolidadoTotalDTO
    {
        public string IdProducto { get; set; }
        public string Producto { get; set; }
        public double TotalDespachadoBruto { get; set; }
        public double TotalDespachadoNeto { get; set; }
        public double TemperaturaPonderadaTotal{ get; set; }
        public double DensidadPonderadaTotal { get; set; }


    }
}
