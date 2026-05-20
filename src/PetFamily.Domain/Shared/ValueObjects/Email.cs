using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed class Email : ComparableValueObject
{
    public const int MAX_LENGTH = 100;
    public const string PATTERN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    private static readonly Regex _regex = new(PATTERN, RegexOptions.Compiled);

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Error.Validation("email.empty", "Email cannot be empty");

        var trimmed = value.Trim();

        if (trimmed.Length > MAX_LENGTH)
            return Error.Validation("email.too_long", $"Email must be at most {MAX_LENGTH} characters");

        if (!_regex.IsMatch(trimmed))
            return Error.Validation("email.invalid_format", "Email has invalid format");

        return new Email(trimmed);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
