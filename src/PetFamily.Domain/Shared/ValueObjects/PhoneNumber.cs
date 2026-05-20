using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed class PhoneNumber : ComparableValueObject
{
    public const int MIN_LENGTH = 7;
    public const int MAX_LENGTH = 20;
    public const string PATTERN = @"^\+?[0-9\s\-\(\)]+$";

    private static readonly Regex _regex = new(PATTERN, RegexOptions.Compiled);

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Error.Validation("phone.empty", "Phone number cannot be empty");

        var trimmed = value.Trim();

        if (trimmed.Length < MIN_LENGTH)
            return Error.Validation("phone.too_short", $"Phone number must be at least {MIN_LENGTH} characters");

        if (trimmed.Length > MAX_LENGTH)
            return Error.Validation("phone.too_long", $"Phone number must be at most {MAX_LENGTH} characters");

        if (!_regex.IsMatch(trimmed))
            return Error.Validation("phone.invalid_format", "Phone number has invalid format");

        return new PhoneNumber(trimmed);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
