using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.Collections;

namespace Validation.CustomValidationAttributes
{
    public class MinimumListLengthAttribute : ValidationAttribute
    {
        private readonly int minNumberofElements;

        public MinimumListLengthAttribute(int minNumberofElements)
        {
            this.minNumberofElements = minNumberofElements;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IList list;
            if (value is IList)
            {
                list = value as IList;
                bool result = list?.Count >= minNumberofElements;

                return result
                    ? ValidationResult.Success
                    : new ValidationResult($"{validationContext.DisplayName} requires at least {minNumberofElements} element" + (minNumberofElements > 1 ? "s" : string.Empty));
            }
            return ValidationResult.Success;
        }
    }
}
