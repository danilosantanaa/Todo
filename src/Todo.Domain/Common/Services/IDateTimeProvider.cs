namespace Todo.Domain.Common.Services;

public interface IDateTimeProvider
{
    public DateTime Now { get; init; }
}