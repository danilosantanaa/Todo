using FluentAssertions;

using Todo.Domain.Menu.ValueObjects;

using menuNamespace = Todo.Domain.Menu;

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

        var menu = menuNamespace.Menu.Create(menuId, nome, iconUrl);

        // Assert
        menu.Nome.Should().Be(nome);
        menu.IconUrl.Should().Be(iconUrl);
    }
}