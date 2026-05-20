using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed class Address : ComparableValueObject
{
    public string City { get; } = default!;
    public string Street { get; } = default!;
    public string? HouseNumber { get; }
    public string? ApartmentNumber { get; }

    private Address (string city, string street, string? houseNumber, string? apartmentNumber)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        ApartmentNumber = apartmentNumber;
    }

    public static Result<Address, Error> Create(string city, string street, string? houseNumber, string? apartmentNumber)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Error.Validation("address.city.empty", $"{nameof(city)} is not be empty");
        if (string.IsNullOrWhiteSpace(street))
            return Error.Validation("address.street.empty", $"{nameof(street)} is not be empty");
        if (string.IsNullOrWhiteSpace(houseNumber))
            return Error.Validation("address.house_number.empty", $"{nameof(houseNumber)} is not be empty");
        if (string.IsNullOrWhiteSpace(apartmentNumber))
            return Error.Validation("address.apartment_number.empty", $"{nameof(apartmentNumber)} is not be empty");

        return new Address(city, street, houseNumber, apartmentNumber);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return City;
        yield return Street;
        yield return HouseNumber == null ? "" : HouseNumber;
        yield return ApartmentNumber == null ? "" : ApartmentNumber;
    }
}
