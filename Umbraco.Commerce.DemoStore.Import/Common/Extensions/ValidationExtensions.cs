using System.ComponentModel.DataAnnotations;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static Core.Models.Validation.ValidationResult ValidateObject(this object o)
        {
            Core.Models.Validation.ValidationResult validationResult = new Core.Models.Validation.ValidationResult();
            ValidationContext validationContext = new ValidationContext(o);
            List<ValidationResult> list = new List<ValidationResult>();
            if (!Validator.TryValidateObject(o, validationContext, list, validateAllProperties: true))
            {
                validationResult.ErrorMessages.AddRange(list.Select((ValidationResult s) => s.ErrorMessage));
            }
            return validationResult;
        }
    }
}
