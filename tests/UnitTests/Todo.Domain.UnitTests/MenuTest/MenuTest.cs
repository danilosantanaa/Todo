using FluentAssertions;

using Todo.Domain.Menus;
using Todo.Domain.Menus.ValueObjects;

namespace Todo.Domain.UnitTests.MenuTest;

public class MenuTest
{
    public void Menu_Deve_Ser_Criado()
    {
        // Arrange
        string nome = "Terefa do dia";
        string iconUrl = "teste/teste.jpg";

        // Act
        var menuId = MenuId.Create();

        var menu = Menu.Create(menuId, nome, iconUrl);

        // Assert
        menu.Nome.Should().Be(nome);
        menu.IconUrl.Should().Be(iconUrl);
    }
}