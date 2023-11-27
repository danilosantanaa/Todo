using Todo.Domain.Common.Models;

namespace Todo.Domain.Todos.ValueObjects;

public sealed class TodoId : ValueObject
{
    public Guid Value { get; private set; }

    private TodoId(Guid value)
    {
        Value = value;
    }

    public static TodoId Create()
    {
        return Create(Guid.NewGuid());
    }

    public static TodoId Create(Guid id)
    {
        return new TodoId(id);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}