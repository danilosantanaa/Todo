namespace Todo.Domain.Common.Errors;

public abstract class DomainError : Exception
{
    protected DomainError(string messagem) : base(messagem)
    {
    }
    protected DomainError() { }
}