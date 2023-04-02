using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionProductosViewModel
    {
        public GestionProductosViewModel()
        {
            EstablecerTablasCorreción();
        }

        public string Titulo { get; set; }
        public string Accion { get; set; }
        public bool Lectura { get; set; }

        public string Icono { get; set; }

        [Required(ErrorMessage = "El código de producto es obligatorio")]
        [MaxLength(40, ErrorMessage = "La longitud máxima son 32 caracteres")]
        public string IdProducto { get; set; }
        [Required(ErrorMessage = "El código SICOM es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string Sicom { get; set; }

        [Required(ErrorMessage = "El Nombre corto de producto es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud maxima son 50 caracteres")]
        public string NombreCorto { get; set; }

        [Required(ErrorMessage = "El Nombre ERP de producto es obligatorio")]
        [MaxLength(250, ErrorMessage = "La longitud mínima son 250 caracteres")]
        public string NombreErp { get; set; }

        [Required(ErrorMessage = "El Tipo de producto es obligatorio")]
        public string IdTipo { get; set; }
        [Required(ErrorMessage = "La clase de producto es obligatorio")]
        public string IdClase { get; set; }
        public string TablaCorreccion { get; set; }
        public List<SelectListItem> TipoProducto { get; set; }
        public List<SelectListItem> ClaseProducto { get; set; }
        public List<SelectListItem> TablasCorreccion { get; set; }
        public bool Estado { get; set; }
        public bool EsAditivado { get; set; }
        public bool AditivadoDespues { get; set; }
        [Required(ErrorMessage = "Se requiere mínimo una receta")]
        public List<RecetaDTO> Recetas { get; set; }
        public List<SelectListItem> ProductosComponentes { get; set; }


        public List<TProductosAtributo> ExtraerAtributos()
        {
            return new List<TProductosAtributo>()
            {
                new TProductosAtributo()
                {
                    IdAtributo = 10001,
                    IdProducto = IdProducto,
                    ValorTexto = EsAditivado.ToString(),
                    EditadoPor = "Admin",
                    UltimaEdicion = DateTime.Now
                },
                new TProductosAtributo()
                {
                    IdAtributo = 10002,
                    IdProducto = IdProducto,
                    ValorTexto = TablaCorreccion,
                    EditadoPor = "Admin",
                    UltimaEdicion = DateTime.Now
                },
                new TProductosAtributo()
                {
                    IdAtributo = 10003,
                    IdProducto = IdProducto,
                    ValorTexto = AditivadoDespues.ToString(),
                    EditadoPor = "Admin",
                    UltimaEdicion = DateTime.Now
                }
            };
        }

        public void EstablecerAtributos(List<TProductosAtributo> atributos)
        {
            TablaCorreccion = atributos?.Where(e => e.IdAtributo == 10002).Select(i => i.ValorTexto).FirstOrDefault();
            bool.TryParse(atributos?.Where(e => e.IdAtributo == 10003).Select(i => i.ValorTexto).FirstOrDefault(), out bool result10003);
            bool.TryParse(atributos?.Where(e => e.IdAtributo == 10001).Select(i => i.ValorTexto).FirstOrDefault(), out bool result10001);
            AditivadoDespues = result10003;
            EsAditivado = result10001;
        }

        private void EstablecerTablasCorreción()
        {
            TablasCorreccion = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "T_API_Correccion_5B", Text = "T_API_Correccion_5B" },
                new SelectListItem() { Value = "T_API_Correccion_6B", Text = "T_API_Correccion_6B" },
                new SelectListItem() { Value = "T_API_Correccion_6C_Alcohol", Text = "T_API_Correccion_6C_Alcohol" }
            };
        }
    }

}
