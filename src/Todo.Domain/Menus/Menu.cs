using Todo.Domain.Common.Models;
using Todo.Domain.Menus.ValueObjects;

namespace Todo.Domain.Menus;

public class Menu : AggregateRoot<MenuId>
{
    public string Nome { get; private set; }
    public string IconUrl { get; private set; }

    private Menu(MenuId id, string nome, string iconUrl) : base(id)
    {
        Nome = nome;
        IconUrl = iconUrl;
    }

    public static Menu Create(MenuId id, string nome, string iconUrl)
    {
        return new Menu(id, nome, iconUrl);
    }
}