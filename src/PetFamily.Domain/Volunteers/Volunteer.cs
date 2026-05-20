using CSharpFunctionalExtensions;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Domain.Volunteers;

public sealed class Volunteer : Entity<VolunteerId>
{
    private List<Pet> _pets = [];
    public IReadOnlyList<Pet> Pets => _pets;
    public SocialNetworkDetails SocialNetworkDetails { get; private set; } = new();

    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public PhoneNumber Phone { get; private set; } = default!;
    public int? ExperienceInYears { get; private set; }
    public RequisiteDetails? RequisitesDetails { get; private set; }

    public int NumberOfPets => _pets.Count;
    public int PetsFoundHome => _pets.Where(d => d.Status.Value == PetStatus.Status.FoundHome).Count();
    public int PetsSeekingHome => _pets.Where(d => d.Status.Value == PetStatus.Status.HomeSeeking).Count();
    public int PetsNeedHelp => _pets.Where(d => d.Status.Value == PetStatus.Status.NeedHelp).Count();

    // for EF Core
    private Volunteer(VolunteerId id) : base(id) { }

    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        PhoneNumber phone,
        int experienceInYears,
        RequisiteDetails requisiteDetails) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        Phone = phone;
        ExperienceInYears = experienceInYears;
        RequisitesDetails = requisiteDetails;
    }

    public void AddSocialNetwork(SocialNetwork socialNetwork)
    {
        SocialNetworkDetails.SocialNetworks.Add(socialNetwork);
    }

    public void RemoveSocialNetwork(SocialNetwork socialNetwork)
    {
        SocialNetworkDetails.SocialNetworks.Remove(socialNetwork);
    }

    public void AddPet(Pet pet)
    {
        _pets.Add(pet);
    }

    public static Result<Volunteer, Error> Create(
        VolunteerId volunteerId,
        FullName fullName,
        Email email,
        Description description,
        PhoneNumber phone,
        int experienceInYears,
        RequisiteDetails requisiteDetails)
    {
        if (experienceInYears < 0)
            return Error.Validation("volunteer.experience.negative", "Experience cannot be negative");

        return new Volunteer(
            volunteerId,
            fullName,
            email,
            description,
            phone,
            experienceInYears,
            requisiteDetails);
    }
}
