using FluentAssertions;

using Todo.Domain.Menus;
using Todo.Domain.Menus.ValueObjects;

namespace Todo.Domain.UnitTests.MenuTest;

public class MenuTest
{
    [Fact]
    public void Menu_Deve_Ser_Criado()
    {
        // Arrange
        string nome = "Terefa do dia";
        string iconUrl = "teste.jpg";
        var menuId = MenuId.Create();

        // Act
        var menu = Menu.Create(menuId, nome, iconUrl);

        // Assert
        menu.Nome.Should().Be(nome);
        menu.IconUrl.Should().Be(iconUrl);
    }

    [Fact]
    public void Menu_Deve_Ser_Atualizado()
    {
        // Arrange
        string nome = "Tarefa do dia";
        string iconUrl = "teste.png";
        Menu menu = Menu.Create(MenuId.Create(), nome, iconUrl);
        MenuId menuId = menu.Id;

        // Act
        menu.Update(nome, iconUrl);

        // Assert
        menu.Nome.Should().Be(nome);
        menu.IconUrl.Should().Be(iconUrl);
        menu.Id.Should().Be(menuId);
    }
}