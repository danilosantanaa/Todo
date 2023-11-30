using Mapster;

using Todo.Application.Todos.Commands.Create;
using Todo.Contracts.Todo.Request;

namespace Todo.Api.Common.Mappings;

public class TodoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TodoRequest, TodoCreateCommand>();
        config.NewConfig<TodoEtapaRequest, TodoEtapaCommand>();
    }
}