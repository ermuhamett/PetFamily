using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public sealed class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _repository;

    public CreateVolunteerHandler(IVolunteersRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var requisites = new RequisiteDetails();
        if (command.Requisites is not null)
        {
            foreach (var dto in command.Requisites)
            {
                var requisiteResult = Requisites.Create(dto.Name, dto.Description);
                if (requisiteResult.IsFailure)
                    return requisiteResult.Error;
                requisites.RequisitesList.Add(requisiteResult.Value);
            }
        }

        var volunteerResult = Volunteer.Create(
            VolunteerId.NewId(),
            command.FullName,
            command.Email,
            command.Description,
            command.Phone,
            command.ExperienceInYears,
            requisites);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var volunteer = volunteerResult.Value;

        if (command.SocialNetworks is not null)
        {
            foreach (var dto in command.SocialNetworks)
            {
                var snResult = SocialNetwork.Create(dto.Link, dto.Title);
                if (snResult.IsFailure)
                    return snResult.Error;
                volunteer.AddSocialNetwork(snResult.Value);
            }
        }

        var id = await _repository.Add(volunteer, cancellationToken);
        return id;
    }
}
