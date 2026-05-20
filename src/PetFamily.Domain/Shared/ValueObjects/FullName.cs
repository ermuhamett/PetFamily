using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed class FullName : ComparableValueObject
{
    public const int MIN_LENGTH = 2;
    public const int MAX_LENGTH = 100;

    public string Value { get; }

    private FullName(string value)
    {
        Value = value;
    }

    public static Result<FullName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Error.Validation("full_name.empty", "Full name cannot be empty");

        var trimmed = value.Trim();

        if (trimmed.Length < MIN_LENGTH)
            return Error.Validation("full_name.too_short", $"Full name must be at least {MIN_LENGTH} characters");

        if (trimmed.Length > MAX_LENGTH)
            return Error.Validation("full_name.too_long", $"Full name must be at most {MAX_LENGTH} characters");

        return new FullName(trimmed);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
