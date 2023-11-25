using Todo.Domain.Common.Models;
using Todo.Domain.Todo.Enums;
using Todo.Domain.Todo.ValueObjects;

namespace Todo.Domain.Todo;

public class Todo : AggregateRoot<TodoId>
{
    public string Descricao { get; set; }
    public TodoTipo Tipo { get; set; }
    public TodoRepeticaoTipo RepeticaoTipo { get; set; }
    public DateTime DataConclusao { get; set; }
    public TodoStatus Status { get; set; }
    public DateTime DataHoraLembrar { get; set; }

    private Todo(
        TodoId todoId,
        string descricao,
        TodoTipo tipo,
        TodoRepeticaoTipo repeticaoTipo,
        DateTime dataConclusao,
        TodoStatus status,
        DateTime dataHoraLembrar) : base(todoId)
    {
        Descricao = descricao;
        Tipo = tipo;
        RepeticaoTipo = repeticaoTipo;
        DataConclusao = dataConclusao;
        Status = status;
        DataHoraLembrar = dataHoraLembrar;
    }

    public static Todo Create(
        string descricao,
        TodoTipo tipo,
        TodoRepeticaoTipo repeticaoTipo,
        DateTime dataConclusao,
        TodoStatus status,
        DateTime dataHoraLembrar)
    {
        return new Todo(
            TodoId.Create(),
            descricao,
            tipo,
            repeticaoTipo,
            dataConclusao,
            status,
            dataHoraLembrar);
    }

}