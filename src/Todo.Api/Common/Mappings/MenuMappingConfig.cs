using Mapster;

using Todo.Application.Menus.Commands.Create;
using Todo.Application.Menus.Commands.Update;
using Todo.Contracts.Menu.Requests;
using Todo.Contracts.Menu.Response;

namespace Todo.Api.Common.Mappings;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MenuRequest, MenuCreateCommand>();

        config.NewConfig<(Guid id, MenuCreateCommand command), MenuResponse>()
            .Map(dst => dst, src => src.command)
            .Map(dst => dst.Id, src => src.id);

        config.NewConfig<(Guid id, MenuRequest request), MenuUpdateCommand>()
            .Map(dst => dst, src => src.request)
            .Map(dst => dst.Id, src => src.id);
    }
}