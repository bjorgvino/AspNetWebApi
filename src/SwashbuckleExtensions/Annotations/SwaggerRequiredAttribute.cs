using System.ComponentModel.DataAnnotations;

namespace AspNetWebApi.SwashbuckleExtensions.Annotations
{
    /// <summary>
    /// This attribute marks properties, fields and parameters as non-optional in the generated
    /// Swagger Docs without generating validation errors when they are missing. The use case for this is when validation 
    /// is being applied explicitly, for example using a FluentValidation validator, instead of relying on data annotations 
    /// for validation.
    /// </summary>
    public class SwaggerRequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// Bypass validation, we don't care whether the value is set or not.
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>True</returns>
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
