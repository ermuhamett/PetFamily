namespace PetFamily.Domain.Species;

public readonly record struct BreedId(Guid Value) : IComparable<BreedId>
{
    public static BreedId NewId() => new(Guid.NewGuid());

    public static BreedId Empty() => new(Guid.Empty);

    public static BreedId Create(Guid id) => new(id);

    public int CompareTo(BreedId other) => Value.CompareTo(other.Value);

    public override string ToString() => Value.ToString();
}
