using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed class Description : ComparableValueObject
{
    public const int MAX_LENGTH = 1000;

    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Error.Validation("description.empty", "Description cannot be empty");

        var trimmed = value.Trim();

        if (trimmed.Length > MAX_LENGTH)
            return Error.Validation("description.too_long", $"Description must be at most {MAX_LENGTH} characters");

        return new Description(trimmed);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
