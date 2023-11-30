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
        MenuId menuId,
        DateTime dataConclusao = default,
        DateTime dataHoraLembrar = default
        ) : base(todoId)
    {
        Descricao = descricao;
        Tipo = tipo;
        RepeticaoTipo = repeticaoTipo;
        Status = TodoStatus.Pendente;
        MenuId = menuId;

        DataConclusao = dataConclusao == DateTime.MinValue ? DateTime.Now : dataConclusao;
        DataHoraLembrar = dataHoraLembrar;
    }

#pragma warning disable CS8618
    private Todo() { }
#pragma warning restore CS8618

    public static Todo Create(
        string descricao,
        TodoTipo tipo,
        TodoRepeticaoTipo repeticaoTipo,
        MenuId menuId,
        DateTime dataConclusao = default,
        DateTime dataHoraLembrar = default)
    {
        return new Todo(
            TodoId.Create(),
            descricao,
            tipo,
            repeticaoTipo,
            menuId,
            dataConclusao,
            dataHoraLembrar);
    }

    public void AddEtapa(string descricao, DateTime dateTimeProvider, DateTime dataExpiracao = default)
    {
        if (Tipo == TodoTipo.Geral)
        {
            throw new TodoEtapaNaoPodeSerAdicionadoException();
        }

        var todoEtapa = TodoEtapa.Create(descricao, this, dateTimeProvider);
        todoEtapa.AddDataExpiracao(dataExpiracao);

        _todoEtapas.Append(todoEtapa);

    }

}