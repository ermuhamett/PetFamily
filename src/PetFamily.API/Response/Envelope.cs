using PetFamily.Domain.Shared;

namespace PetFamily.API.Response;

/// <summary>
/// Unified response wrapper. Every endpoint returns this shape:
/// a payload on success, a list of errors on failure, plus a generation timestamp.
/// </summary>
public sealed class Envelope
{
    public object? Result { get; }
    public IReadOnlyList<ResponseError> ErrorInfo { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, IEnumerable<ResponseError> errorInfo)
    {
        Result = result;
        ErrorInfo = errorInfo.ToList();
        TimeGenerated = DateTime.UtcNow;
    }

    public static Envelope Ok(object? result = null) =>
        new(result, []);

    public static Envelope Error(IEnumerable<Error> errors) =>
        new(null, errors.Select(e => new ResponseError(e.Code, e.Message, e.InvalidField)));
}

public sealed record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);
