namespace Todo.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object? obj)
    {

        if (obj is null || obj.GetType() != GetType()) return false;

        var valueObject = (ValueObject)obj;

        return GetAtomicValues()
            .SequenceEqual(valueObject.GetAtomicValues());
    }

    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(right == left);
    }
}