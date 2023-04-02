using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class DespachosDetallesViewModel
    {
        public IEnumerable<string> EncabezadosDetallado { get; set; }
        public IEnumerable<TDespacho> DespachosDetallados { get; set; }

        public DespachosDetallesViewModel()
        {
            EncabezadosDetallado = new List<string>() { "Estado" , "Fecha", "Compañía", "No Orden" , "Cabezote" , "Tráiler" , "Conductor" , "Producto", "Volumen Ordenado", "Volumen Cargado", "Acciones" };
        }        

    }
}