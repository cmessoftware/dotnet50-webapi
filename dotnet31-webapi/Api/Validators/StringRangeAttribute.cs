using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace cmes_webapi.Entities.Common.ValidationsAttribute
{
    /// <summary>
    /// <para>Atributo de Validacion DataAnnotation que establece los valores que se esperan recibir en un campo string. No acepta nulos y no valida mayúsculas y minusculas. </para>
    /// DataAnnotation Validation Attribute that sets the expected values ​​in a string field. Does not accept null and does not accept upper and lower case..
    /// </summary>
    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var msg = ErrorMessage;
            if (value != null)
            {
                string[] allowableValuesToUpper = new string[0];
                foreach (var allowableValue in AllowableValues)
                {
                    Array.Resize(ref allowableValuesToUpper, allowableValuesToUpper.Length + 1);
                    allowableValuesToUpper[allowableValuesToUpper.Length - 1] = allowableValue.ToUpper();
                }
                var valueUpper = value.ToString().ToUpper();
                if (allowableValuesToUpper?.Contains(valueUpper?.ToString()) == true)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(msg);
            }
            return ValidationResult.Success;
        }
    }
}
