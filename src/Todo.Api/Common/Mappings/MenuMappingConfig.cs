
using System.ComponentModel.Design;

using Mapster;

using Todo.Application.Menu.Commands.Create;
using Todo.Contracts.Menu.Requests;

namespace Todo.Api.Common.Mappings;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MenuRequest, MenuCreateCommand>();
    }
}