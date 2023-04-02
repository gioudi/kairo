using KAIROSV2.Business.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using KAIROSV2.Business.Entities.DTOs;

namespace KAIROSV2.WebApp.Support.ValidationAttributes.Proveedores
{
    public class ProveedorProductosAttribute : ValidationAttribute
    {
        private readonly string _errorMessage;
        public ProveedorProductosAttribute(string ErrorMesagge = default)
        {
            _errorMessage = (string.IsNullOrEmpty(ErrorMessage) ? $"Existen productos repetidos en la lista." : ErrorMessage);
        }

        protected override ValidationResult IsValid(
       object value, ValidationContext validationContext)
        {
            var productos = value as ICollection<ProveedorProductoDTO>;

            if(productos?.Count > 0)
            {
                if (productos.Count != productos?.DistinctBy(p => p.IdTipoProducto)?.Count())
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
