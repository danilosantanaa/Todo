using Todo.Domain.Common.Models;

namespace Todo.Domain.Todo.ValueObjects;

public sealed class TodoEtapaId : ValueObject
{
    public Guid Value { get; private set; }

    private TodoEtapaId(Guid value)
    {
        Value = value;
    }

    public static TodoEtapaId Create()
    {
        return new TodoEtapaId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}