using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class DespachosConsolidadosViewModel
    {
        public IEnumerable<string> EncabezadosConsolidado { get; set; }
        public IEnumerable<DespachosConsolidadosDetalleDTO> DespachosConsolidados { get; set; }
        
        public DespachosConsolidadosViewModel()
        {
            EncabezadosConsolidado = new List<string>() { "Fecha", "Compañia", "Producto", "Volumen Bruto", "Volumen Neto", "Temperatura", "Densidad", "Factor" };
        }        

    }
}