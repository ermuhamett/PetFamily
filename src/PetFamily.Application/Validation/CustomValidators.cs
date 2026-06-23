using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Validation;

public static class CustomValidators
{
    /// <summary>
    /// Validates a property by running it through a Value Object factory method.
    /// Reuses the domain validation rules instead of duplicating them in the validator.
    /// On failure the domain <see cref="Error"/> is serialized into the failure message
    /// so it can be reconstructed later.
    /// </summary>
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }

    /// <summary>
    /// Attaches a domain <see cref="Error"/> to a rule, serializing it into the failure message
    /// so the same conversion pipeline applies to non Value Object rules.
    /// </summary>
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        Error error)
    {
        return rule.WithMessage(error.Serialize());
    }
}
