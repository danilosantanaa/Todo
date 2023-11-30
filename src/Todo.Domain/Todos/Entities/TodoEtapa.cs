using Todo.Domain.Common.Models;
using Todo.Domain.Todos.Errors;
using Todo.Domain.Todos.ValueObjects;

namespace Todo.Domain.Todos.Entities;

public sealed class TodoEtapa : Entity<TodoEtapaId>
{
    public string Descricao { get; private set; }
    public DateTime DataExpiracao { get; private set; }
    public bool Concluido { get; private set; }

    public TodoId TodoId { get; private set; }

#pragma warning disable CS8618
    private TodoEtapa(
        TodoEtapaId id,
        string descricao,
        TodoId todoId,
        DateTime dateTimeProvider) : base(id)
    {
        Descricao = descricao;
        TodoId = todoId;
        AddDataExpiracao(dateTimeProvider);
        Concluido = false;
    }

    private TodoEtapa() { }
#pragma warning restore CS8618

    public static TodoEtapa Create(string descricao, Todo todo, DateTime dateTimeProvider)
    {
        return new(TodoEtapaId.Create(), descricao, todo.Id, dateTimeProvider);
    }

    public void AddDataExpiracao(DateTime dateTimeProvider, DateTime dataExpiracao = default)
    {
        DataExpiracao =
            dataExpiracao == DateTime.MinValue || dateTimeProvider == dataExpiracao ?
                dateTimeProvider.AddDays(1)
                : dataExpiracao;

        if (DataExpiracao < dateTimeProvider)
        {
            throw new TodoEtapaDataExpiracaoInvalidoException();
        }

    }
}