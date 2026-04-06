using CSharpFunctionalExtensions;
using PetFamily.Domain.Species;

namespace PetFamily.Domain.Pets
{
    public sealed class PetBreed : ComparableValueObject
    {
        public SpeciesId SpeciesId { get; }
        public BreedId BreedId { get; }

        private PetBreed() { }

        private PetBreed(SpeciesId speciesId, BreedId breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<PetBreed> Create(SpeciesId speciesId, BreedId breedId)
        {
            if (speciesId.Equals(SpeciesId.Empty()))
                return Result.Failure<PetBreed>($"{nameof(speciesId)} cannot be empty");

            if (breedId.Equals(BreedId.Empty()))
                return Result.Failure<PetBreed>($"{nameof(breedId)} cannot be empty");

            return Result.Success(new PetBreed(speciesId, breedId));
        }
        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return SpeciesId.Value;
            yield return BreedId.Value;
        }
    }
}
