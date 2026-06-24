using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Response;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error) =>
        error.ToErrors().ToResponse();

    public static ActionResult ToResponse(this ErrorList errors)
    {
        var errorList = errors.ToList();

        if (errorList.Count == 0)
            return new ObjectResult(Envelope.Error(errorList))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

        var distinctTypes = errorList.Select(e => e.Type).Distinct().ToList();

        // Mixed error types can't map to a single HTTP status -> treat as a server error.
        var statusCode = distinctTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeForErrorType(distinctTypes.First());

        return new ObjectResult(Envelope.Error(errorList)) { StatusCode = statusCode };
    }

    private static int GetStatusCodeForErrorType(ErrorType errorType) => errorType switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };
}
