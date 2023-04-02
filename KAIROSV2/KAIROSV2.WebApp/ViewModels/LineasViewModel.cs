using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class LineasViewModel
    {
        public ActionsPermission ActionsPermission { get; set; }
        public IEnumerable<LineaDTO> Lineas { get; set; }
        //public IEnumerable<string> Encabezados { get; set; }

        //public IEnumerable<WidthHeader> Encabezados { get; set; }

        public IEnumerable<WidthHeader> Encabezados { get; set; }


        public IEnumerable<string> WHeaders { get; set; }

        public LineasViewModel()
        {
            //Encabezados = new List<string>(){ "Terminal", "Línea", "Producto", "Estado", "Volumen - gal", "Densidad Aforo - API", "Observaciones", "Acciones" };
            
            //Encabezados = new List<WidthHeader>() { Encabezado = "Terminal", width = "150px" };
           this.Encabezados= new List<WidthHeader> {  new WidthHeader() { Encabezado = "Terminal", Width = "150px"},
                                                       new WidthHeader() { Encabezado = "Línea", Width = "150px"},
                                                       new WidthHeader() { Encabezado = "Producto", Width = "150px"},
                                                       new WidthHeader() { Encabezado = "Estado", Width = "150px"},
                                                       new WidthHeader() { Encabezado = "Volumen - gal", Width = "150px"},
                                                       new WidthHeader() { Encabezado = "Densidad Aforo - API", Width = "150px"},
                                                       new WidthHeader() { Encabezado = "Observaciones", Width = "150px"},
                                                       new WidthHeader() { Encabezado = "Acciones", Width = "150px"}
                };



        }
    }

    public class WidthHeader
    {
        public string Encabezado { get; set; }
        public string Width { get; set; }
    }
}
