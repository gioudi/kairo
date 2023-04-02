using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.WebApp.Support.ValidationAttributes.Proveedores;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class GestionProveedorViewModel
    {
        public string Titulo { get; set; }
        public string Accion { get; set; }
        public bool Lectura { get; set; }

        [Required(ErrorMessage = "El Id del proveedor es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string IdProveedor { get; set; }

        [Required(ErrorMessage = "El Nombre del proveedor es obligatorio")]
        [MaxLength(150, ErrorMessage = "La longitud minima son 10 caracteres")]
        public string NombreProveedor { get; set; }

        [Required(ErrorMessage = "El SICOM del proveedor es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string SicomProveedor { get; set; }
        public List<SelectListItem> Tipos { get; set; }
        [ProveedorPlantas]
        public List<TProveedoresPlanta> ProveedoresPlanta { get; set; }
        [ProveedorProductos]
        public List<ProveedorProductoDTO> ProveedoresProductos { get; set; }
    }
}
