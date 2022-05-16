using System.ComponentModel.DataAnnotations;

namespace Gateways.Common.Validators;

public class IPv4Attribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        try
        {
            if (value == null)
                return ValidationResult.Success;
            var errorResult = new ValidationResult("Value is not a valid IPv4");
            if (!(value is string))
                return errorResult;
            var data = (string)value;
            var dataSplited = data.Split(".");
            if (dataSplited.Length != 4)
                return errorResult;
            foreach (var item in dataSplited)
            {
                if (item == string.Empty)
                    return errorResult;
                if (item.Trim() != item)
                    return errorResult;
                if (item[0] == '0' && item.Length > 1)
                    return errorResult;
                if (!int.TryParse(item, out int result))
                    return errorResult;
                if (result < 0 || result > 255)
                    return errorResult;
            }
            return ValidationResult.Success;
        }
        catch (Exception)
        {
            return new ValidationResult($"Error processing {value}");
        }
    }
}