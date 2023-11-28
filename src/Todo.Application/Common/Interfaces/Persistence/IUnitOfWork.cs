using System.Diagnostics;

namespace Todo.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    IMenuRepository MenuRepository { get; set; }
    ITodoRepository TodoRepository { get; set; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}