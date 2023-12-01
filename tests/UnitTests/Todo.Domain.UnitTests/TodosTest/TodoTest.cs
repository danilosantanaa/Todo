using FluentAssertions;

using Moq;

using Todo.Domain.Common.Services;
using Todo.Domain.Menus.ValueObjects;
using Todo.Domain.Todos.Enums;
using Todo.Domain.Todos.Errors;
using Todo.Domain.Todos.ValueObjects;

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

        _todoGlobal =
        TodoDomain.Todo.Create(
            "Descricao",
            TodoTipo.Geral,
            TodoRepeticaoTipo.UmaVez,
            MenuId.Create(),
            _dateTimeProviderMock.Object);
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

        // Assert
        todo.Descricao.Should().Be(descricao);
        todo.Tipo.Should().Be(tipo);
        todo.RepeticaoTipo.Should().Be(repeticaoTipo);
        todo.MenuId.Should().Be(menuId);
        todo.DataConclusao.Should().Be(_dateTimeProviderMock.Object.Now);
    }

    [Theory]
    [MemberData(nameof(GetTodoAtualizacaoExperimentos))]
    public void Todo_Deve_Ser_Atualizado(
        string descricao_antiga,
        TodoTipo tipo_antigo,
        TodoRepeticaoTipo repeticaoTipo_antigo,
        string descricao_nova,
        TodoTipo tipo_novo,
        TodoRepeticaoTipo repeticaoTipo_novo)
    {
        // Arrange
        MenuId menuId = MenuId.Create();

        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create(descricao_antiga, tipo_antigo, repeticaoTipo_antigo, menuId, _dateTimeProviderMock.Object);
        todo.Update(descricao_nova, tipo_novo, repeticaoTipo_novo);

        // Assert 
        todo.Descricao.Should().Be(descricao_nova);
        todo.Tipo.Should().Be(tipo_novo);
        todo.RepeticaoTipo.Should().Be(repeticaoTipo_novo);
        todo.MenuId.Should().Be(menuId);
    }

    [Fact]
    public void Todo_Nao_Pode_Adicionar_Etapa_Quando_For_Geral()
    {
        // Arrange
        string todo_descricao = "Descricao";
        TodoTipo todoTipo = TodoTipo.Geral;
        TodoRepeticaoTipo todoRepeticaoTipo = TodoRepeticaoTipo.UmaVez;
        MenuId menuId = MenuId.Create();

        string etapa_descricao = "Etapa 1";
        DateTime dataExpiracao = new DateTime(2023, 11, 19, 10, 0, 0);

        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create(
            todo_descricao,
            todoTipo,
            todoRepeticaoTipo,
            menuId,
            _dateTimeProviderMock.Object);

        Action action = () => todo.AddEtapa(etapa_descricao, _dateTimeProviderMock.Object, dataExpiracao);

        // Assert
        action.Should().Throw<TodoEtapaNaoPodeSerAdicionadoException>();
    }

    [Fact]
    public void Todo_Deve_Adicionar_Etapa()
    {
        // Arrange
        string todo_descricao = "Descricao";
        TodoTipo todoTipo = TodoTipo.Etapa;
        TodoRepeticaoTipo todoRepeticaoTipo = TodoRepeticaoTipo.UmaVez;
        MenuId menuId = MenuId.Create();

        string etapa_descricao = "Etapa 1";
        DateTime dataExpiracao = new DateTime(2023, 11, 20, 10, 0, 0);

        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create(
            todo_descricao,
            todoTipo,
            todoRepeticaoTipo,
            menuId,
            _dateTimeProviderMock.Object);

        var result = todo.AddEtapa(etapa_descricao, _dateTimeProviderMock.Object, dataExpiracao);


        // Assert
        todo.TodoEtapas.Should().NotBeNullOrEmpty()
        .And.Contain(result);

        result.Descricao.Should().Be(etapa_descricao);
        result.Concluido.Should().BeFalse();
        result.DataExpiracao.Should().Be(dataExpiracao.AddDays(1));
    }

    [Fact]
    public void Todo_Nao_Pode_Atualizar_Etapa_Quando_For_Geral()
    {
        // Arrange 
        string todo_descricao = "Descricao";
        TodoTipo todoTipo = TodoTipo.Geral;
        TodoRepeticaoTipo todoRepeticaoTipo = TodoRepeticaoTipo.UmaVez;
        MenuId menuId = MenuId.Create();

        TodoEtapaId todoEtapaId = TodoEtapaId.Create();
        string etapa_descricao = "Etapa 1";
        DateTime dataExpiracao = new DateTime(2023, 11, 19, 10, 0, 0);

        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create(
            todo_descricao,
            todoTipo,
            todoRepeticaoTipo,
            menuId,
            _dateTimeProviderMock.Object);

        Action action = () => todo.UpdateEtapa(todoEtapaId, etapa_descricao, _dateTimeProviderMock.Object, dataExpiracao);

        // Assert
        action.Should().Throw<TodoEtapaNaoPodeSerAdicionadoException>();
    }

    [Fact]
    public void Todo_Deve_Atualizar_Etapa()
    {
        // Arrange
        string todo_descricao = "Descricao";
        TodoTipo todoTipo = TodoTipo.Etapa;
        TodoRepeticaoTipo todoRepeticaoTipo = TodoRepeticaoTipo.UmaVez;
        MenuId menuId = MenuId.Create();


        string etapa_descricao_antiga = "Etapa 1";
        DateTime dataExpiracao = new DateTime(2023, 11, 20, 10, 0, 0);

        string etapa_descricao_novo = "Etapa 1";

        // Act
        TodoDomain.Todo todo = TodoDomain.Todo.Create(
            todo_descricao,
            todoTipo,
            todoRepeticaoTipo,
            menuId,
            _dateTimeProviderMock.Object);

        var todoEtapa = todo.AddEtapa(etapa_descricao_antiga, _dateTimeProviderMock.Object, dataExpiracao);

        var result = todo.UpdateEtapa(todoEtapa.Id, etapa_descricao_novo, _dateTimeProviderMock.Object, dataExpiracao);

        todo.TodoEtapas.Should().NotBeNullOrEmpty()
            .And.Contain(result);

        result.Descricao.Should().Be(etapa_descricao_novo);
        result.Concluido.Should().BeFalse();
        result.DataExpiracao.Should().Be(dataExpiracao.AddDays(1));
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
    [MemberData(nameof(GetMinDateTime))]
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

    public static IEnumerable<object[]> GetMinDateTime()
    {
        yield return new object[] { 1, 1, 1, 0, 0, 0 };
    }

    public static IEnumerable<object[]> GetTodoAtualizacaoExperimentos()
    {
        yield return new object[] {
            "FAZER X", TodoTipo.Etapa, TodoRepeticaoTipo.UmaVez,
            "FAZR X1", TodoTipo.Etapa, TodoRepeticaoTipo.UmaVez
        };

        yield return new object[] {
            "FAZER Y", TodoTipo.Etapa, TodoRepeticaoTipo.UmaVez,
            "FAZR Y1", TodoTipo.Geral, TodoRepeticaoTipo.UmaVez
        };

        yield return new object[] {
            "FAZER W", TodoTipo.Geral, TodoRepeticaoTipo.UmaVez,
            "FAZR W1", TodoTipo.Etapa, TodoRepeticaoTipo.UmaVez
        };
    }
}