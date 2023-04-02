using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using KAIROSV2.WebApp.Models;
using KAIROSV2.WebApp.Support.Util;
using KAIROSV2.WebApp.Support.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionTerminalCompañiaViewModel
    {
        
        public string Titulo { get; set; }
        public string Accion { get; set; }
        public bool Lectura { get; set; }

        [Required(ErrorMessage = "Id terminal es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string IdTerminal { get; set; }

        [Required(ErrorMessage = "Compañía es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud maxima son 50 caracteres")]
        public string Compañia { get; set; }

        public string IdCompañia { get; set; }

        [Required(ErrorMessage = "Estado es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string Estado { get; set; }                

        [Required(ErrorMessage = "Sicom Compañia Terminal es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string SicomCompañiaTerminal { get; set; }

        public bool Socia { get; set; }
        
        [Required(ErrorMessage = "Id Compañia Agrupadora es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud maxima son 50 caracteres")]
        public string CompañiaAgrupadora { get; set; }

        [Required(ErrorMessage = "Porcentaje Propiedad es obligatorio")]
        //[MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public double PorcentajePropiedad { get; set; }

        public List<SelectListItem> CompañiasAgrupadora { get; set; }

        public void AsignarCompañia(TTerminalCompañia _Compañia)
        {
            IdTerminal = _Compañia?.IdTerminal;
            IdCompañia = _Compañia?.IdCompañia;
            Estado = _Compañia?.IdCompañia;
            SicomCompañiaTerminal = _Compañia?.SicomCompañiaTerminal;
            Socia = _Compañia.Socia;
            CompañiaAgrupadora = _Compañia?.IdCompañiaAgrupadora;
            PorcentajePropiedad = _Compañia.PorcentajePropiedad;

    }

        public TTerminalCompañia ExtraerCompañia()
        {
            var _Compañia = new TTerminalCompañia()
            {
                IdTerminal = IdTerminal,
                IdCompañia = IdCompañia,
                Estado = Estado,
                SicomCompañiaTerminal = SicomCompañiaTerminal,
                Socia = Socia,
                IdCompañiaAgrupadora = CompañiaAgrupadora,
                PorcentajePropiedad = PorcentajePropiedad

            };

            return _Compañia;

        }

    }
}


