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
        var fullNameResult = FullName.Create(command.FullName);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
            return emailResult.Error;

        var descriptionResult = Description.Create(command.Description);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var phoneResult = PhoneNumber.Create(command.Phone);
        if (phoneResult.IsFailure)
            return phoneResult.Error;

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
            fullNameResult.Value,
            emailResult.Value,
            descriptionResult.Value,
            phoneResult.Value,
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
