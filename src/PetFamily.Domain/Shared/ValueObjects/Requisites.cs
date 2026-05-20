using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects
{
    public sealed class Requisites : ComparableValueObject
    {
        public string Name { get; }
        public string Description { get; }

        private Requisites (string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Result<Requisites, Error> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Error.Validation("requisites.name.empty", $"{nameof(name)} is not be empty");

            return new Requisites(name, description);
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}
