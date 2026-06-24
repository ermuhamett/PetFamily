using FluentValidation.Results;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Validation;

public static class ValidationExtensions
{
    /// <summary>
    /// Converts every FluentValidation failure into a domain <see cref="Error"/>.
    /// Messages produced by <see cref="CustomValidators"/> are serialized errors and are
    /// deserialized back; any other message is wrapped as a generic validation error.
    /// The failing property name is attached as <see cref="Error.InvalidField"/>.
    /// </summary>
    public static ErrorList ToErrors(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(failure =>
        {
            var error = failure.ErrorMessage.Contains(Error.Separator)
                ? Error.Deserialize(failure.ErrorMessage)
                : Error.Validation(failure.PropertyName, failure.ErrorMessage);

            return error.WithInvalidField(failure.PropertyName);
        });

        return new ErrorList(errors);
    }
}
