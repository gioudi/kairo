using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class DespachosConsolidadosDetalleDTO
    {
        public DateTime Fecha { get; set; }
        public string IdCompañia { get; set; }
        public string Compañia { get; set; }
        public string IdProducto { get; set; }
        public string Producto { get; set; }
        public double VolumenUnitarioBruto { get; set; }
        public double VolumenUnitarioNeto { get; set; }
        public double PorcentajePonderado { get; set; }
        public double TemperaturaPonderada { get; set; }
        public double DensidadPonderada { get; set; }
        public double Factor { get; set; }

    }
}
