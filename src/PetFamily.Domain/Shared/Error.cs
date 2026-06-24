namespace PetFamily.Domain.Shared;

public sealed record Error
{
    public const string Separator = "||";

    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }

    public static Error Validation(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.Validation, invalidField);

    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    public static Error Failure(string code, string message) =>
        new(code, message, ErrorType.Failure);

    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);

    public Error WithInvalidField(string? invalidField) =>
        new(Code, Message, Type, invalidField);

    public ErrorList ToErrors() => new([this]);

    public string Serialize() => string.Join(Separator, Code, Message, Type);

    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(Separator);
        if (parts.Length < 3)
            throw new ArgumentException("Invalid serialized error", nameof(serialized));

        if (!Enum.TryParse<ErrorType>(parts[2], out var type))
            throw new ArgumentException("Invalid error type", nameof(serialized));

        return new Error(parts[0], parts[1], type);
    }
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}
