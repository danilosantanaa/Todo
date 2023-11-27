using Todo.Domain.Common.Models;

namespace Todo.Domain.Menus.ValueObjects;

public sealed class MenuId : ValueObject
{
    public Guid Value { get; }

    private MenuId(Guid value)
    {
        Value = value;
    }

    public static MenuId Create()
    {
        return Create(Guid.NewGuid());
    }

    public static MenuId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}