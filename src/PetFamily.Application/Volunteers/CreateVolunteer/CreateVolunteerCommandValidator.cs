using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Contracts.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public sealed class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.FullName).MustBeValueObject(FullName.Create);
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.Phone).MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => c.ExperienceInYears)
            .GreaterThanOrEqualTo(0)
            .WithError(Error.Validation("volunteer.experience.negative", "Experience cannot be negative"));

        RuleForEach(c => c.Requisites).SetValidator(new RequisiteDtoValidator());
        RuleForEach(c => c.SocialNetworks).SetValidator(new SocialNetworkDtoValidator());
    }
}

public sealed class RequisiteDtoValidator : AbstractValidator<RequisiteDto>
{
    public RequisiteDtoValidator()
    {
        RuleFor(r => r).MustBeValueObject(r => Requisites.Create(r.Name, r.Description));
    }
}

public sealed class SocialNetworkDtoValidator : AbstractValidator<SocialNetworkDto>
{
    public SocialNetworkDtoValidator()
    {
        RuleFor(s => s).MustBeValueObject(s => SocialNetwork.Create(s.Link, s.Title));
    }
}
