
using FluentAssertions;

using Todo.Domain.Todos.Entities;
using Todo.Domain.Todos.Errors;

namespace Todo.Domain.UnitTests.TodosTest.Entities;

public class TodoEtapaTest
{
    [Fact]
    public void TodoEtapa_Deve_Ser_Criado()
    {
        // Arrange
        string descricao = "Lista 1";
        DateTime dateTimeProvider = new DateTime(2023, 11, 20, 10, 0, 0);

        // Act
        TodoEtapa todoEtapa = TodoEtapa.Create(descricao, dateTimeProvider);

        // Assert
        todoEtapa.Descricao.Should().Be(descricao);
        todoEtapa.Concluido.Should().Be(false);
        todoEtapa.DataExpiracao.Should().Be(dateTimeProvider.AddDays(1));
    }

    [Theory]
    [MemberData(nameof(GetDateTimeExpiracoes))]
    public void TodoEtapa_Criada_Com_Error_Data_Expiracao_Menor_Que_DataTime_Atual(int ano, int mes, int dia, int hora, int minuto, int segundo)
    {
        // Arrange
        string descricao = "Lista 1";
        DateTime dateTimeProvider = new DateTime(2023, 11, 20, 10, 0, 0);
        DateTime dataExpiracao = new DateTime(ano, mes, dia, hora, minuto, segundo);

        // Act
        TodoEtapa todoEtapa = TodoEtapa.Create(descricao, dateTimeProvider);

        Action action = () => todoEtapa.AddDataExpiracao(dateTimeProvider, dataExpiracao);

        // Assert
        action.Should().Throw<TodoEtapaDataExpiracaoInvalidoException>();
    }

    public static IEnumerable<object[]> GetDateTimeExpiracoes()
    {
        yield return new object[] { 2023, 11, 19, 0, 0, 0 };
        yield return new object[] { 2023, 11, 20, 9, 0, 0 };
        yield return new object[] { 2023, 11, 20, 9, 59, 0 };
        yield return new object[] { 2023, 11, 20, 9, 59, 59 };
    }
}