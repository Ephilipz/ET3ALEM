using System.Collections;
using System.ComponentModel.DataAnnotations;

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
            if (value is not IList list) return ValidationResult.Success;
            
            var result = list.Count >= minNumberofElements;

            return result
                ? ValidationResult.Success
                : new ValidationResult(
                    $"{validationContext.DisplayName} requires at least {minNumberofElements} element" +
                    (minNumberofElements > 1 ? "s" : string.Empty));

        }
    }
}