using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class DespachosViewModel
    {
        public DateTime FechaActual { get; set; }
        public List<SelectListItem> Terminales { get; set; }
        public string Terminal { get; set; }
        public List<SelectListItem> Compañias { get; set; }
        public string Compañia { get; set; }
        public string FechaCorte { get; set; }
        public Boolean PermiteEditar { get; set; }
        public IEnumerable<string> EncabezadosConsolidado { get; set; }
        public IEnumerable<string> EncabezadosDetallado { get; set; }
        public IEnumerable<DespachosConsolidadosDTO> DespachosConsolidados { get; set; }
        public IEnumerable<DespachosDetalladosDTO> DespachosDetallados { get; set; }
        public ActionsPermission ActionsPermission { get; set; }

        public DespachosViewModel()
        {
            EncabezadosConsolidado = new List<string>() { "Id_Producto", "Producto" ,"Volumen_Ordenado - Gal", "Volumen_Cargado - Gal", "Diferencia - Gal" };
            EncabezadosDetallado = new List<string>() { "Estado Off/On", "Fecha", "Compañía", "No Orden", "Compartimento", "Cabezote" , "Tráiler" , "Conductor" , "Producto", "Volumen Ordenado - Gal", "Volumen Cargado - Gal", "Acciones" };
        }        

    }
}