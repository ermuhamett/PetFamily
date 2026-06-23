using System.Collections;

namespace PetFamily.Domain.Shared;

/// <summary>
/// An immutable collection of <see cref="Error"/>. Lets handlers return several
/// errors at once (e.g. all FluentValidation failures) instead of just the first.
/// Implicit conversions allow returning a single <see cref="Error"/> or a list directly.
/// </summary>
public sealed class ErrorList : IEnumerable<Error>
{
    private readonly IReadOnlyList<Error> _errors;

    public ErrorList(IEnumerable<Error> errors)
    {
        _errors = errors.ToList();
    }

    public IEnumerator<Error> GetEnumerator() => _errors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static implicit operator ErrorList(List<Error> errors) => new(errors);

    public static implicit operator ErrorList(Error error) => new([error]);
}
