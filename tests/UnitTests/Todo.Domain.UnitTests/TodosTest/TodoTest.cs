using FluentAssertions;

using Moq;

using Todo.Domain.Common.Services;
using Todo.Domain.Menus.ValueObjects;
using Todo.Domain.Todos.Enums;
using Todo.Domain.Todos.Errors;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Domain.UnitTests.TodosTest;

public class TodoTest
{
    private readonly TodoDomain.Todo _todoGlobal;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

    public TodoTest()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _dateTimeProviderMock
           !.Setup(x => x.Now)
           .Returns(new DateTime(2023, 11, 20, 10, 0, 0));

        _todoGlobal = TodoDomain.Todo.Create("Descricao", TodoTipo.Geral, TodoRepeticaoTipo.UmaVez, MenuId.Create(), _dateTimeProviderMock.Object);
    }

    [Theory]
    [InlineData("Fazer X", TodoTipo.Etapa, TodoRepeticaoTipo.UmaVez)]
    [InlineData("Fazer Y", TodoTipo.Geral, TodoRepeticaoTipo.UmaVez)]
    public void Todo_Deve_Ser_Criado(string descricao, TodoTipo tipo, TodoRepeticaoTipo repeticaoTipo)
    {
        // Arrange
        MenuId menuId = MenuId.Create();

        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create(descricao, tipo, repeticaoTipo, menuId, _dateTimeProviderMock.Object);

        //Assert
        todo.Descricao.Should().Be(descricao);
        todo.Tipo.Should().Be(tipo);
        todo.RepeticaoTipo.Should().Be(repeticaoTipo);
        todo.MenuId.Should().Be(menuId);
        todo.DataConclusao.Should().Be(_dateTimeProviderMock.Object.Now);
    }

    [Fact]
    public void Todo_Nao_Pode_Adicionar_Etapa_Quando_For_Geral()
    {
        // Arrange
        DateTime dataExpiracao = new DateTime(2023, 11, 19, 10, 0, 0);

        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create("descricao", TodoTipo.Geral, TodoRepeticaoTipo.UmaVez, MenuId.Create(), _dateTimeProviderMock.Object);
        Action action = () => todo.AddEtapa("Etapa 1", _dateTimeProviderMock.Object, dataExpiracao);

        // Assert
        action.Should().Throw<TodoEtapaNaoPodeSerAdicionadoException>();
    }

    [Theory]
    [MemberData(nameof(GetDataEHorasExpiradas))]
    public void TodoAdicionarDataConclusao_Deve_Lancar_Erro_Quando_For_Menor_Que_Data_E_Hora_Atual(
        int ano,
        int mes,
        int dia,
        int hora,
        int minuto,
        int segundo
    )
    {
        // Arrange
        DateTime dataConclusao = new DateTime(ano, mes, dia, hora, minuto, segundo);

        // Act
        Action action = () => _todoGlobal.AddDataConclusao(_dateTimeProviderMock.Object, dataConclusao);

        // Assert
        action.Should().Throw<TodoDataConclusaoMenorQueDateTimeAtual>();
    }

    [Theory]
    [MemberData(nameof(GetDataEHorasExpiradas))]
    public void TodoAdicionarDataHoraLembrar_Deve_Lancar_Erro_Quando_For_Menor_Que_Data_E_Hora_Atual(
        int ano,
        int mes,
        int dia,
        int hora,
        int minuto,
        int segundo
    )
    {
        // Arrange
        DateTime dataHoraLembrar = new DateTime(ano, mes, dia, hora, minuto, segundo);

        // Act
        Action action = () => _todoGlobal.AddDataHoraLembrar(_dateTimeProviderMock.Object, dataHoraLembrar);

        // Assert
        action.Should().Throw<TodoDataHoraLembrarMenorQueDateTimeAtual>();
    }

    public static IEnumerable<object[]> GetDataEHorasExpiradas()
    {
        yield return new object[] { 2023, 11, 19, 0, 0, 0 };
        yield return new object[] { 2023, 11, 20, 9, 0, 0 };
        yield return new object[] { 2023, 11, 20, 9, 59, 0 };
        yield return new object[] { 2023, 11, 20, 9, 59, 59 };
    }
}