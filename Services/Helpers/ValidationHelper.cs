using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        internal static void ModelValidation (Object obj)
        {
            // Model Validations
            // stores reference to model object
            ValidationContext validationContext = new(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            // validates entire model object
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            // if this is false that means there is an error could be one or more
            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
