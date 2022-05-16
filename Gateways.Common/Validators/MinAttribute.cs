using System.ComponentModel.DataAnnotations;

namespace Gateways.Common.Validators;

public class MinAttribute : ValidationAttribute
{
    private int MinimumValue { get; }

    public MinAttribute(int minimumValue) => MinimumValue = minimumValue;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        try
        {
            if (value != null && value is int && MinimumValue > (int)value)
            {
                return new ValidationResult($"Value must be greater than {MinimumValue}");
            }

            return ValidationResult.Success;
        }
        catch (Exception)
        {
            return new ValidationResult($"Error processing {value}");
        }
    }
}