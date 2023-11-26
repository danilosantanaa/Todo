using Mapster;

using Todo.Application.Menus.Commands.Create;
using Todo.Contracts.Menu.Requests;

namespace Todo.Api.Common.Mappings;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MenuRequest, MenuCreateCommand>();
    }
}