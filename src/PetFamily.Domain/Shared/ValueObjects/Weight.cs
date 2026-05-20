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

        public static Result<Weight, Error> Create(decimal value)
        {
            if (value <= 0)
                return Error.Validation("weight.invalid", "Weight must be greater than zero");

            if (value > 500)
                return Error.Validation("weight.out_of_range", "Weight is out of valid range");

            return new Weight(decimal.Round(value, 2));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => $"{Value:0.##} {Unit}";
    }
}
