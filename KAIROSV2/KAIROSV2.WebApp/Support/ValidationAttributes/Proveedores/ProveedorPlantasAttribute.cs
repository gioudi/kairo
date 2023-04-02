using KAIROSV2.Business.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;

namespace KAIROSV2.WebApp.Support.ValidationAttributes.Proveedores
{
    public class ProveedorPlantasAttribute : ValidationAttribute
    {
        private readonly string _errorMessage;
        public ProveedorPlantasAttribute(string ErrorMesagge = default)
        {
            _errorMessage = (string.IsNullOrEmpty(ErrorMessage) ? $"Existen plantas repetidas en la lista." : ErrorMessage);
        }

        protected override ValidationResult IsValid(
       object value, ValidationContext validationContext)
        {
            var plantas = value as ICollection<TProveedoresPlanta>;

            if(plantas?.Count > 0)
            {
                if (plantas.Count != plantas.DistinctBy(p => new { p.PlantaProveedor, p.SicomPlantaProveedor })?.Count())
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return _errorMessage;
        }
    }
}
