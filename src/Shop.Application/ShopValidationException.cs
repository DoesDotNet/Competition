using FluentValidation.Results;

namespace Shop.Application;

public class ShopValidationException : Exception
{
    public ShopValidationException()
    {
        Errors = new Dictionary<string, string>();
    }
    
    public ShopValidationException(ValidationResult results)
    {
        Errors = results.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage);
    }

    public Dictionary<string, string> Errors { get; }
}