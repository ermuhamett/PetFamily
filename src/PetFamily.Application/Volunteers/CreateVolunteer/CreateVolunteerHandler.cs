using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public sealed class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _repository;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteerHandler(
        IVolunteersRepository repository,
        IValidator<CreateVolunteerCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrors();

        var fullNameResult = FullName.Create(command.FullName);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error.ToErrors();

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
            return emailResult.Error.ToErrors();

        var descriptionResult = Description.Create(command.Description);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error.ToErrors();

        var phoneResult = PhoneNumber.Create(command.Phone);
        if (phoneResult.IsFailure)
            return phoneResult.Error.ToErrors();

        var requisites = new RequisiteDetails();
        foreach (var dto in command.Requisites)
        {
            var requisiteResult = Requisites.Create(dto.Name, dto.Description);
            if (requisiteResult.IsFailure)
                return requisiteResult.Error.ToErrors();
            requisites.RequisitesList.Add(requisiteResult.Value);
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
            return volunteerResult.Error.ToErrors();

        var volunteer = volunteerResult.Value;

        foreach (var dto in command.SocialNetworks)
        {
            var snResult = SocialNetwork.Create(dto.Link, dto.Title);
            if (snResult.IsFailure)
                return snResult.Error.ToErrors();
            volunteer.AddSocialNetwork(snResult.Value);
        }

        var id = await _repository.Add(volunteer, cancellationToken);
        return id;
    }
}
