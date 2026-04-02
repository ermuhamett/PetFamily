using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects
{
    public sealed class Weight : ComparableValueObject
    {
        public decimal Value { get; }
        public string Unit => "kg";

        private Weight(decimal value)
        {
            Value = value;
        }

        public static Result<Weight> Create(decimal value)
        {
            if (value <= 0)
                return Result.Failure<Weight>("Weight must be greater than zero");

            if (value > 500)
                return Result.Failure<Weight>("Weight is out of valid range");

            return Result.Success(new Weight(decimal.Round(value, 2)));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => $"{Value:0.##} {Unit}";
    }
}
