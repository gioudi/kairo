using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Entities.DTOs
{
    public class DespachosDetalladosDTO
    {
        public string Id_Despacho { get; set; }
        public bool Estado_Kairos { get; set; }        
        public string Id_Compañia { get; set; }
        public string No_Orden { get; set; }
        public string Compartimento { get; set; }
        public string Id_Producto_Despacho { get; set; }
        public DateTime Fecha_Final_Despacho { get; set; }
        public int? Cedula_Conductor { get; set; }
        public string Placa_Cabezote { get; set; }
        public string Placa_Trailer { get; set; }
        public double Volumen_Ordenado { get; set; }
        public double Volumen_Cargado { get; set; }        

    }
}
