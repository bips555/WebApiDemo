using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Models.Validations
{
    public class Shirt_EnsureCorrectSizingAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var shirt = validationContext.ObjectInstance as Shirt;
            if (shirt != null && !String.IsNullOrEmpty(shirt.Gender))
            {
                if(shirt.Gender.Equals("men",StringComparison.OrdinalIgnoreCase) && shirt.Size < 8)
                    {
                    return new ValidationResult("For mens shirt, size should be greater than 8");   
                }
                else if(shirt.Gender.Equals("women",StringComparison.OrdinalIgnoreCase) && shirt.Size < 6) {
                return new ValidationResult("For womens shirt, size should be greater than 6");
                }

            }
            return ValidationResult.Success;


        }
    }
}
