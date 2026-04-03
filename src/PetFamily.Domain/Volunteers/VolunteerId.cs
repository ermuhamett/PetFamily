namespace PetFamily.Domain.Volunteers;

public readonly record struct VolunteerId(Guid Value) : IComparable<VolunteerId>
{
    public static VolunteerId NewId() => new(Guid.NewGuid());
    public static VolunteerId Empty() => new(Guid.Empty);
    public static VolunteerId Create(Guid value) => new(value);

    public int CompareTo(VolunteerId other) => Value.CompareTo(other.Value);

    public override string ToString() => Value.ToString();
}