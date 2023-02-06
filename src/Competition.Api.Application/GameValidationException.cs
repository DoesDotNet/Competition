using FluentValidation.Results;

namespace Shop.Application;

public class GameValidationException : Exception
{
    public GameValidationException(string message):
        base(message)
    {
    }
    
    public GameValidationException(ValidationResult results)
    {
        Errors = results.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage);
    }

    public Dictionary<string, string> Errors { get; } = new();
}
