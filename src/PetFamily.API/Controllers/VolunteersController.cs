using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Contracts.Volunteers;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class VolunteersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var command = CreateVolunteerCommand.FromRequest(request);
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToActionResult();

        return Ok(result.Value);
    }
}
