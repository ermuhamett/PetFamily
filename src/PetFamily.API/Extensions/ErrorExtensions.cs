using Microsoft.AspNetCore.Mvc;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Extensions;

public static class ErrorExtensions
{
    public static ActionResult ToActionResult(this Error error) => error.Type switch
    {
        ErrorType.Validation => new BadRequestObjectResult(new { error.Code, error.Message }),
        ErrorType.NotFound => new NotFoundObjectResult(new { error.Code, error.Message }),
        ErrorType.Conflict => new ConflictObjectResult(new { error.Code, error.Message }),
        _ => new ObjectResult(new { error.Code, error.Message }) { StatusCode = StatusCodes.Status500InternalServerError }
    };
}
