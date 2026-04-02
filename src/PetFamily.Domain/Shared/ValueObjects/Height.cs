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
    public static Result<Height> Create(decimal value)
    {
        if (value <= 0)
            return Result.Failure<Height>("Height must be greater than zero");

        if (value > 300)
            return Result.Failure<Height>("Height is out of valid range");

        return Result.Success(new Height(decimal.Round(value, 2)));
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
       yield return Value;
    }
    public override string ToString() => $"{Value:0.##} {Unit}";
}
