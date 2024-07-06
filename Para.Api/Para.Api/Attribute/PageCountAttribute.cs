using System.ComponentModel.DataAnnotations;

namespace Para.Api;

public class PageCountAttribute : ValidationAttribute
{
    public PageCountAttribute()
    {
            
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var model = (Book)validationContext.ObjectInstance;
        ValidationResult result = ValidationResult.Success;
        var pageCount = (int)value;
        if (model.Year >= 1900 && model.Year <= 1950)
        {
            if (pageCount > 100)
            {
                return new ValidationResult("Invalid page count for Year " + model.Year);
            }
        }

        if (model.Year >= 1951 && model.Year <= 1999)
        {
            if (pageCount > 200)
            {
                return new ValidationResult("Invalid page count for Year " + model.Year);
            }
        }
            
        return ValidationResult.Success;
    }
}
