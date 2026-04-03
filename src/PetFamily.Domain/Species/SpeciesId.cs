namespace PetFamily.Domain.Species;

public readonly record struct SpeciesId(Guid Value) : IComparable<SpeciesId>
{
    public static SpeciesId NewId() => new(Guid.NewGuid());
    public static SpeciesId Empty() => new(Guid.Empty);
    public static SpeciesId Create(Guid value) => new(value);

    public int CompareTo(SpeciesId other) => Value.CompareTo(other.Value);

    public override string ToString() => Value.ToString();
}