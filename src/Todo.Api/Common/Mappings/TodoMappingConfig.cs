using Mapster;

using Todo.Application.Todos.Commands.Create;
using Todo.Contracts.Todo.Request;
using Todo.Contracts.Todo.Response;
using Todo.Domain.Todos.Entities;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Api.Common.Mappings;

public class TodoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TodoRequest, TodoCreateCommand>();
        config.NewConfig<TodoEtapaRequest, TodoEtapaCommand>();

        config.NewConfig<TodoDomain.Todo, TodoResponse>()
            .Map(dst => dst, src => src)
            .Map(dst => dst.MenuId, src => src.MenuId.Value)
            .Map(dst => dst.Id, src => src.Id.Value);

        config.NewConfig<TodoEtapa, TodoEtapaResponse>()
            .Map(dst => dst, src => src)
            .Map(dst => dst.Id, src => src.Id.Value);
    }
}