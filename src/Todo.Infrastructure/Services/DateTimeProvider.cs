using Todo.Application.Common.Interfaces.Services;

namespace Todo.Infrastructure.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; init; } = DateTime.UtcNow;
}