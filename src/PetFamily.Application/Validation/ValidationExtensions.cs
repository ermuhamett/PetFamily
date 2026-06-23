using FluentValidation.Results;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Validation;

public static class ValidationExtensions
{
    /// <summary>
    /// Converts the first FluentValidation failure into a domain <see cref="Error"/>.
    /// Messages produced by <see cref="CustomValidators"/> are serialized errors and are
    /// deserialized back; any other message is wrapped as a generic validation error.
    /// </summary>
    public static Error ToError(this ValidationResult validationResult)
    {
        var failure = validationResult.Errors.First();
        var message = failure.ErrorMessage;

        if (message.Contains(Error.Separator))
            return Error.Deserialize(message);

        return Error.Validation(failure.PropertyName, message);
    }
}
