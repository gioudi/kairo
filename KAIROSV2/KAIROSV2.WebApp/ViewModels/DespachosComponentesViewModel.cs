using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Models
{
    public class DespachosComponentesViewModel
    {
        public DespachosComponentesViewModel()
        {
            Tanques = new List<SelectListItem>();
            Contadores = new List<SelectListItem>();
            Ship_Tos = new List<SelectListItem>();
            Productos = new List<SelectListItem>();
        }

        public bool Lectura { get; set; }

        [Required(ErrorMessage = "El Producto es obligatorio")]
        public string Producto_Componente { get; set; }

        [Required(ErrorMessage = "El Volumen Bruto es obligatorio")]
        //[Range(1, 1000000, ErrorMessage = "El rango del Volumen Bruto es de 1 a 1000000")]
        public double Volumen_Bruto { get; set; }

        [Required(ErrorMessage = "La Temperatura es obligatoria")]
        [Range(32, 150, ErrorMessage = "El rango de la Temperatura es de 32 a 150")]
        public double Temperatura { get; set; }

        [Required(ErrorMessage = "La Densidad es obligatoria")]
        [Range(10, 100, ErrorMessage = "El rango de la Densidad es de 10 a 100")]
        public double Densidad { get; set; }
        public double Factor { get; set; }

        [Required(ErrorMessage = "El Volumen Neto es obligatorio")]
        [Range(1, 1000000, ErrorMessage = "El rango del Volumen Neto es de 1 a 1000000")]
        public double Volumen_Neto { get; set; }

        [Required(ErrorMessage = "El Tanque es obligatorio")]
        public string Tanque { get; set; }

        [Required(ErrorMessage = "El Contador es obligatorio")]
        public string Contador { get; set; }

        [Required(ErrorMessage = "La Código de Cliente es obligatorio")]
        [Range(1, 10000000, ErrorMessage = "El Código de Cliente es de 1 a 1000000")]
        public int Ship_To { get; set; }
        
        public List<SelectListItem> Productos { get; set; }
        public List<SelectListItem> Tanques { get; set; }
        public List<SelectListItem> Contadores { get; set; }
        public List<SelectListItem> Ship_Tos { get; set; }

    }
}