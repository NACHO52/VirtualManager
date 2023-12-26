using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //Here Namespace used for access attributes.  
using System.Linq;
using System.Web;

namespace VirtualManager.Shared
{
    public class CustomValidation
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class GreaterThanZero : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    decimal _value = Convert.ToDecimal(value);
                    if (_value <= 0)
                    {
                        return new ValidationResult("Value must be greater than zero.");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}