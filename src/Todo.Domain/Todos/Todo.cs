using Todo.Domain.Common.Models;
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

    public IReadOnlyList<TodoEtapa> TodoEtapas => _todoEtapas.ToList().AsReadOnly();

    private Todo(
        TodoId todoId,
        string descricao,
        TodoTipo tipo,
        TodoRepeticaoTipo repeticaoTipo,
        DateTime dataConclusao,
        TodoStatus status,
        DateTime dataHoraLembrar,
        MenuId menuId) : base(todoId)
    {
        Descricao = descricao;
        Tipo = tipo;
        RepeticaoTipo = repeticaoTipo;
        DataConclusao = dataConclusao;
        Status = status;
        DataHoraLembrar = dataHoraLembrar;
        MenuId = menuId;
    }

#pragma warning disable CS8618
    private Todo() { }
#pragma warning restore CS8618

    public static Todo Create(
        string descricao,
        TodoTipo tipo,
        TodoRepeticaoTipo repeticaoTipo,
        DateTime dataConclusao,
        TodoStatus status,
        DateTime dataHoraLembrar,
        MenuId menuId)
    {
        return new Todo(
            TodoId.Create(),
            descricao,
            tipo,
            repeticaoTipo,
            dataConclusao,
            status,
            dataHoraLembrar,
            menuId);
    }

    public TodoEtapa AddEtapa(string descricao, DateTime dataExpiracao)
    {
        if (Tipo == TodoTipo.Geral)
        {
            throw new TodoListaNaoPodeSerAdicionadoException();
        }

        var todoEtapa = TodoEtapa.Create(descricao, dataExpiracao, false);
        _todoEtapas.Append(todoEtapa);
        return todoEtapa;
    }

}