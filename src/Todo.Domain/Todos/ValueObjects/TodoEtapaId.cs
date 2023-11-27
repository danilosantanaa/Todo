using Todo.Domain.Common.Models;

namespace Todo.Domain.Todos.ValueObjects;

public sealed class TodoEtapaId : ValueObject
{
    public Guid Value { get; private set; }

    private TodoEtapaId(Guid value)
    {
        Value = value;
    }

    public static TodoEtapaId Create()
    {
        return Create(Guid.NewGuid());
    }

    public static TodoEtapaId Create(Guid id)
    {
        return new TodoEtapaId(id);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}