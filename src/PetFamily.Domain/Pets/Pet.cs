using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Domain.Pets;

public sealed class Pet : Entity<PetId>
{
    public const int NAME_MAX_LENGTH = Constants.MAX_LOW_TEXT_LENGTH;
    public const int COLOR_MAX_LENGTH = Constants.MAX_LOW_TEXT_LENGTH;

    public string Name { get; private set; } = default!;
    public PetBreed Breed { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public Weight Weight { get; private set; } = default!;
    public Height Height { get; private set; } = default!;
    public Description HealthInformation { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public PhoneNumber Phone { get; private set; } = default!;
    public bool IsCastrated { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; }
    public PetStatus Status { get; private set; } = default!;
    public RequisiteDetails? RequisiteDetails { get; private set; } = new();
    public DateTime CreatedDate { get; private set; } = DateTime.Now;

    // for EF Core
    private Pet(PetId id) : base(id) { }

    private Pet(
        PetId id,
        string name,
        PetBreed breed,
        Description description,
        string color,
        Height height,
        Weight weight,
        Description healthInformation,
        Address address,
        PhoneNumber phone,
        bool isCastrated,
        DateOnly birthDate,
        bool isVaccinated,
        PetStatus status,
        RequisiteDetails requisiteDetails) : base(id)
    {
        Name = name;
        Breed = breed;
        Description = description;
        Color = color;
        Height = height;
        Weight = weight;
        HealthInformation = healthInformation;
        Address = address;
        Phone = phone;
        IsCastrated = isCastrated;
        BirthDate = birthDate;
        IsVaccinated = isVaccinated;
        Status = status;
        RequisiteDetails = requisiteDetails;
    }

    public static Result<Pet, Error> Create(
        PetId id,
        string name,
        PetBreed breed,
        Description description,
        string color,
        Height height,
        Weight weight,
        Description healthInformation,
        Address address,
        PhoneNumber phone,
        bool isCastrated,
        DateOnly birthDate,
        bool isVaccinated,
        PetStatus status,
        RequisiteDetails requisiteDetails)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("pet.name.empty", "Pet name cannot be empty");

        if (name.Length > NAME_MAX_LENGTH)
            return Error.Validation("pet.name.too_long", $"Pet name must be at most {NAME_MAX_LENGTH} characters");

        if (string.IsNullOrWhiteSpace(color))
            return Error.Validation("pet.color.empty", "Pet color cannot be empty");

        if (color.Length > COLOR_MAX_LENGTH)
            return Error.Validation("pet.color.too_long", $"Pet color must be at most {COLOR_MAX_LENGTH} characters");

        return new Pet(
            id,
            name.Trim(),
            breed,
            description,
            color.Trim(),
            height,
            weight,
            healthInformation,
            address,
            phone,
            isCastrated,
            birthDate,
            isVaccinated,
            status,
            requisiteDetails);
    }
}
