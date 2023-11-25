using FluentAssertions;

using Todo.Domain.Menu.ValueObjects;

namespace Todo.Domain.UnitTests.Menu.ValueObjects;

public class MenuIdTest
{
    [Fact]
    public void MenuId_Deve_Ser_Criado()
    {
        // Arrange
        Guid valueEmpty = Guid.Empty;

        // Act
        var menuId = MenuId.Create();

        // Assert
        menuId.Should().NotBe(valueEmpty);
    }

    [Fact]
    public void MenuIdGetAtomicValues_Deve_RetornarValue()
    {
        // Arrange
        Guid valueEmpty = Guid.Empty;

        // Act
        var menuId = MenuId.Create();
        var result = menuId.GetAtomicValues();

        // Assert
        result.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.NotContainInOrder(new[] { valueEmpty })
            .And.ContainItemsAssignableTo<Guid>();
    }
}