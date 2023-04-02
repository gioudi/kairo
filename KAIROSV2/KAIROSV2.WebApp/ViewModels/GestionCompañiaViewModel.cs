using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
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
    public class GestionCompañiaViewModel
    {
        public string Titulo { get; set; }

        public string Accion { get; set; }
                
        [Required(ErrorMessage = "Id Compañia es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud máxima son 10 caracteres")]
        public string IdCompañia { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [MinLength(3, ErrorMessage = "La longitud minima son 3 caracteres")]
        [MaxLength(30, ErrorMessage = "La longitud máxima son 30 caracteres")]
        public string Compañia { get; set; }

        [Required(ErrorMessage = "Sales Organization es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string SalesOrganization { get; set; }

        [Required(ErrorMessage = "Distribution Channel es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string DistributionChannel { get; set; }

        [Required(ErrorMessage = "Division es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string Division { get; set; }

        [Required(ErrorMessage = "Supplier Type es obligatorio")]
        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string SupplierType { get; set; }

        [MaxLength(10, ErrorMessage = "La longitud maxima son 10 caracteres")]
        public string CodigoSICOM { get; set; }

        public bool Lectura { get; set; }

        public void AsignarCompañia(TCompañia _Compañia)
        {
            IdCompañia = _Compañia?.IdCompañia;
            Compañia = _Compañia?.Nombre;
            SalesOrganization = _Compañia?.SalesOrganization;
            DistributionChannel = _Compañia?.DistributionChannel;
            Division = _Compañia?.Division;
            SupplierType = _Compañia?.SupplierType;
            CodigoSICOM = _Compañia?.CodigoSicom;
           
        }

        public TCompañia ExtraerCompañia()
        {
            var _Compañia = new TCompañia()
            {
                IdCompañia = IdCompañia,
                Nombre = Compañia,
                SalesOrganization = SalesOrganization,
                DistributionChannel = DistributionChannel,
                Division = Division,
                SupplierType = SupplierType,
                CodigoSicom = CodigoSICOM,
                EditadoPor = "Admin",
                UltimaEdicion = DateTime.Now
            };           

            return _Compañia;
                
        }

    }
}
