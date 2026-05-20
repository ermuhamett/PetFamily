using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;



public sealed class Height : ComparableValueObject
{
    public decimal Value { get; }
    public string Unit => "cm";

    private Height(decimal value) 
    {
        Value = value;  
    }
    public static Result<Height, Error> Create(decimal value)
    {
        if (value <= 0)
            return Error.Validation("height.invalid", "Height must be greater than zero");

        if (value > 300)
            return Error.Validation("height.out_of_range", "Height is out of valid range");

        return new Height(decimal.Round(value, 2));
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
       yield return Value;
    }
    public override string ToString() => $"{Value:0.##} {Unit}";
}
