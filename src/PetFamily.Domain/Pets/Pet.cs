using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Domain.Pets;

public sealed class Pet:Entity<PetId>
{
    public string Name { get; private set; }
    public PetBreed Breed { get; private set; }
    public string Description { get; private set; }
    public string Color { get; private set; }
    public Weight Weight { get; private set; }
    public Height Height { get; private set; }
    public string HealthInformation { get; private set; }
    public Address Address { get; private set; }
    public string Phone { get; private set; }
    public bool IsCastrated { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; }
    public PetStatus Status { get; private set; }
    public Requisites Requisites { get; private set; }
    public DateTime CreatedDate = DateTime.Now;

    private Pet(
        PetId id, 
        string name, 
        PetBreed breed, 
        string description, 
        string color, 
        Height height,
        Weight weight, 
        string healthInformation, 
        Address address, 
        string phone, 
        bool isCastrated,
        DateOnly birthDate, 
        bool isVaccinated, 
        PetStatus status, 
        Requisites requisites) : base(id)
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
        Requisites = requisites;
    }

    public static Result<Pet> Create(
        PetId id, 
        string name, 
        PetBreed breed, 
        string description, 
        string color, 
        decimal heightValue,
        decimal weightValue, 
        string healthInformation, 
        Address address, 
        string phone, 
        bool isCastrated,
        DateOnly birthDate, 
        bool isVaccinated, 
        PetStatus status, 
        Requisites requisites)
    {
        if (string.IsNullOrWhiteSpace(name)) return Result.Failure<Pet>($"Pet name is not be empty");
        var heightResult = Height.Create(heightValue);
        if (heightResult.IsFailure)
            return Result.Failure<Pet>(heightResult.Error);

        var weightResult = Weight.Create(weightValue);
        if (weightResult.IsFailure)
            return Result.Failure<Pet>(weightResult.Error);

        var pet = new Pet(
            id,
            name,
            breed,
            description,
            color,
            heightResult.Value,
            weightResult.Value,
            healthInformation,
            address,
            phone,
            isCastrated,
            birthDate,
            isVaccinated,
            status,
            requisites);

        return Result.Success(pet);
    }
}
