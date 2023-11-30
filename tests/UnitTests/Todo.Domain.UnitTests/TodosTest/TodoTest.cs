using FluentAssertions;

using Todo.Domain.Menus.ValueObjects;
using Todo.Domain.Todos.Enums;
using Todo.Domain.Todos.Errors;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Domain.UnitTests.TodosTest;

public class TodoTest
{
    [Theory]
    [InlineData("Fazer X", TodoTipo.Etapa, TodoRepeticaoTipo.UmaVez)]
    [InlineData("Fazer Y", TodoTipo.Geral, TodoRepeticaoTipo.UmaVez)]
    public void Todo_Deve_Ser_Criado(string descricao, TodoTipo tipo, TodoRepeticaoTipo repeticaoTipo)
    {
        // Arrange
        MenuId menuId = MenuId.Create();

        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create(descricao, tipo, repeticaoTipo, menuId);

        //Assert
        todo.Descricao.Should().Be(descricao);
        todo.Tipo.Should().Be(tipo);
        todo.RepeticaoTipo.Should().Be(repeticaoTipo);
        todo.MenuId.Should().Be(menuId);
    }

    [Fact]
    public void Todo_Nao_Pode_Adicionar_Etapa_Quando_For_Geral()
    {
        // Arrange
        DateTime dateTimeProvider = new DateTime(2023, 11, 19, 10, 0, 0);
        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create("descricao", TodoTipo.Geral, TodoRepeticaoTipo.UmaVez, MenuId.Create());
        Action action = () => todo.AddEtapa("Etapa 1", dateTimeProvider);

        // Assert
        action.Should().Throw<TodoEtapaNaoPodeSerAdicionadoException>();
    }
}