using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Support.ValidationAttributes
{
    public class NotEqualAttribute : ValidationAttribute
    {
        private string _propertyNotEqual { get; set; }
        private readonly string _errorMessage;

        public NotEqualAttribute(string OtherProperty, string ErrorMessage = default)
        {
            _propertyNotEqual = OtherProperty;
            _errorMessage = (string.IsNullOrEmpty(ErrorMessage) ? $"{0} no debe ser igual a {1}." : ErrorMessage);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get other property value
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_propertyNotEqual);
            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            // verify values
            if (value.ToString().Equals(otherValue.ToString()))
                //return new ValidationResult(string.Format("{0} no debe ser igual a {1}.", validationContext.MemberName, _notEqual));
                return new ValidationResult(GetErrorMessage());
            else
                return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return _errorMessage;
        }
    }
}
