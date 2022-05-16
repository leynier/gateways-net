using System.ComponentModel.DataAnnotations;

namespace Gateways.Common.Validators;

public class MaxAttribute : ValidationAttribute
{
    private int MaximumValue { get; }

    public MaxAttribute(int maximumValue) => MaximumValue = maximumValue;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        try
        {
            if (value != null && value is int && MaximumValue < (int)value)
            {
                return new ValidationResult($"Value must be lower than {MaximumValue}");
            }

            return ValidationResult.Success;
        }
        catch (Exception)
        {
            return new ValidationResult($"Error processing {value}");
        }
    }
}