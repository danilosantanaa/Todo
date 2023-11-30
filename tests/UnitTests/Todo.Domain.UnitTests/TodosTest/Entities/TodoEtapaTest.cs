
using FluentAssertions;

using Moq;

using Todo.Domain.Common.Services;
using Todo.Domain.Menus.ValueObjects;
using Todo.Domain.Todos.Entities;
using Todo.Domain.Todos.Errors;

using TodoDomain = Todo.Domain.Todos;

namespace Todo.Domain.UnitTests.TodosTest.Entities;

public class TodoEtapaTest
{
    private readonly TodoDomain.Todo _todo;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

    public TodoEtapaTest()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _dateTimeProviderMock
           !.Setup(x => x.Now)
           .Returns(new DateTime(2023, 11, 20, 10, 0, 0));

        _todo =
            TodoDomain
            .Todo.Create("Descricao",
                TodoDomain.Enums.TodoTipo.Etapa,
                TodoDomain.Enums.TodoRepeticaoTipo.UmaVez,
                MenuId.Create(),
                _dateTimeProviderMock.Object);
    }

    [Fact]
    public void TodoEtapa_Deve_Ser_Criado()
    {
        // Arrange
        string descricao = "Lista 1";

        // Act
        TodoEtapa todoEtapa = TodoEtapa.Create(descricao, _todo, _dateTimeProviderMock!.Object);

        // Assert
        todoEtapa.Descricao.Should().Be(descricao);
        todoEtapa.Concluido.Should().Be(false);
        todoEtapa.DataExpiracao.Should().Be(_dateTimeProviderMock.Object.Now.AddDays(1));
    }

    [Theory]
    [MemberData(nameof(GetDataEHorasExpiradas))]
    public void TodoEtapa_Criada_Com_Error_Data_Expiracao_Menor_Que_DataTime_Atual(int ano, int mes, int dia, int hora, int minuto, int segundo)
    {
        // Arrange
        string descricao = "Lista 1";

        DateTime dataExpiracao = new DateTime(ano, mes, dia, hora, minuto, segundo);

        // Act
        TodoEtapa todoEtapa = TodoEtapa.Create(descricao, _todo, _dateTimeProviderMock.Object);
        Action action = () => todoEtapa.AddDataExpiracao(_dateTimeProviderMock.Object, dataExpiracao);

        // Assert
        action.Should().Throw<TodoEtapaDataExpiracaoInvalidoException>();
    }

    public static IEnumerable<object[]> GetDataEHorasExpiradas()
    {
        yield return new object[] { 2023, 11, 19, 0, 0, 0 };
        yield return new object[] { 2023, 11, 20, 9, 0, 0 };
        yield return new object[] { 2023, 11, 20, 9, 59, 0 };
        yield return new object[] { 2023, 11, 20, 9, 59, 59 };
    }
}