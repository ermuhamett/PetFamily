namespace PetFamily.Domain.Pets;

public readonly record struct PetId(Guid Value) : IComparable<PetId>
{
    public static PetId NewId() => new(Guid.NewGuid());
    public static PetId Empty() => new(Guid.Empty);
    public static PetId Create(Guid value) => new(value);

    public int CompareTo(PetId other) => Value.CompareTo(other.Value);

    public override string ToString() => Value.ToString();
}