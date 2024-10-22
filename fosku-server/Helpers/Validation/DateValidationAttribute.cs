using System.ComponentModel.DataAnnotations;

namespace fosku_server.Helpers.Validation;

public class DateValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        DateTime? date = value as DateTime?;
        if(date.HasValue)
        {
            return date.Value <= DateTime.Now;
        }
        return false;
    }
}