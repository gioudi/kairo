using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Support.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly string _errorMessage;
        public MaxFileSizeAttribute(int maxFileSize, string ErrorMessage = default)
        {
            _maxFileSize = maxFileSize;
            _errorMessage = (string.IsNullOrEmpty(ErrorMessage) ? $"El tamaño maximo del archivo es {_maxFileSize} kb." : ErrorMessage);
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if ((file.Length / 1024 ) > _maxFileSize) //Kb
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
