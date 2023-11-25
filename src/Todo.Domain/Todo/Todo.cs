using Todo.Domain.Common.Models;
using Todo.Domain.Menu.ValueObjects;
using Todo.Domain.Todo.Entities;
using Todo.Domain.Todo.Enums;
using Todo.Domain.Todo.ValueObjects;

namespace Todo.Domain.Todo;

public class Todo : AggregateRoot<TodoId>
{

    private List<TodoEtapa> _todoEtapas = new();

    public string Descricao { get; set; }
    public TodoTipo Tipo { get; set; }
    public TodoRepeticaoTipo RepeticaoTipo { get; set; }
    public DateTime DataConclusao { get; set; }
    public TodoStatus Status { get; set; }
    public DateTime DataHoraLembrar { get; set; }

    public MenuId MenuId { get; set; }

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
        var todoEtapa = TodoEtapa.Create(descricao, dataExpiracao, false);
        _todoEtapas.Append(todoEtapa);
        return todoEtapa;
    }

}