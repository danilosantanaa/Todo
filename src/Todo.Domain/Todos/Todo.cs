using Todo.Domain.Common.Models;
using Todo.Domain.Common.Services;
using Todo.Domain.Menus.ValueObjects;
using Todo.Domain.Todos.Entities;
using Todo.Domain.Todos.Enums;
using Todo.Domain.Todos.Errors;
using Todo.Domain.Todos.ValueObjects;

namespace Todo.Domain.Todos;

public class Todo : AggregateRoot<TodoId>
{

    private readonly List<TodoEtapa> _todoEtapas = new();

    public string Descricao { get; private set; }
    public TodoTipo Tipo { get; private set; }
    public TodoRepeticaoTipo RepeticaoTipo { get; private set; }
    public DateTime DataConclusao { get; private set; }
    public TodoStatus Status { get; private set; }
    public DateTime DataHoraLembrar { get; private set; }

    public MenuId MenuId { get; private set; }

    public IReadOnlyList<TodoEtapa> TodoEtapas => _todoEtapas.AsReadOnly();

    private Todo(
        TodoId todoId,
        string descricao,
        TodoTipo tipo,
        TodoRepeticaoTipo repeticaoTipo,
        MenuId menuId,
        IDateTimeProvider dateTimeProvider
        ) : base(todoId)
    {
        Descricao = descricao;
        Tipo = tipo;
        RepeticaoTipo = repeticaoTipo;
        Status = TodoStatus.Pendente;
        MenuId = menuId;
        AddDataConclusao(dateTimeProvider, dateTimeProvider.Now);
    }

#pragma warning disable CS8618
    private Todo() { }
#pragma warning restore CS8618

    public static Todo Create(
        string descricao,
        TodoTipo tipo,
        TodoRepeticaoTipo repeticaoTipo,
        MenuId menuId,
        IDateTimeProvider dateTimeProvider)
    {
        return new Todo(
            TodoId.Create(),
            descricao,
            tipo,
            repeticaoTipo,
            menuId,
            dateTimeProvider);
    }

    public void Update(string descricao, TodoTipo tipo, TodoRepeticaoTipo repeticaoTipo)
    {
        Descricao = descricao;
        Tipo = tipo;
        RepeticaoTipo = repeticaoTipo;
    }

    public TodoEtapa AddEtapa(string descricao, IDateTimeProvider dateTimeProvider, DateTime dataExpiracao)
    {
        if (Tipo == TodoTipo.Geral)
        {
            throw new TodoEtapaNaoPodeSerAdicionadoException();
        }

        var todoEtapa = TodoEtapa.Create(descricao, this, dateTimeProvider);
        todoEtapa.AddDataExpiracao(dateTimeProvider, dataExpiracao);

        _todoEtapas.Add(todoEtapa);

        return todoEtapa;
    }

    public TodoEtapa UpdateEtapa(TodoEtapaId todoEtapaId, string descricao, IDateTimeProvider dateTimeProvider, DateTime dataExpiracao)
    {
        if (Tipo == TodoTipo.Geral)
        {
            throw new TodoEtapaNaoPodeSerAdicionadoException();
        }

        var todoEtapa = _todoEtapas.Find(x => x.Id == todoEtapaId);
        todoEtapa?.Update(descricao);
        todoEtapa?.AddDataExpiracao(dateTimeProvider, dataExpiracao);

        return todoEtapa!;
    }

    public void AddDataConclusao(IDateTimeProvider dateTimeProvider, DateTime dataConclusao)
    {
        dataConclusao = dataConclusao == DateTime.MinValue ? dateTimeProvider.Now : dataConclusao;
        if (dataConclusao < dateTimeProvider.Now)
        {
            throw new TodoDataConclusaoMenorQueDateTimeAtual();
        }

        DataConclusao = dataConclusao;
    }

    public void AddDataHoraLembrar(IDateTimeProvider dateTimeProvider, DateTime dataHoraLembrar)
    {
        if (dataHoraLembrar < dateTimeProvider.Now)
        {
            throw new TodoDataHoraLembrarMenorQueDateTimeAtual();
        }

        DataHoraLembrar = dataHoraLembrar;
    }

    public bool IsEtapaExists(TodoEtapaId todoEtapaId)
    {
        return _todoEtapas.Any(te => te.Id == todoEtapaId);
    }

}