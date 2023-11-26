using Todo.Domain.Common.Models;
using Todo.Domain.Todos.ValueObjects;

namespace Todo.Domain.Todos.Entities;

public sealed class TodoEtapa : Entity<TodoEtapaId>
{
    public string Descricao { get; private set; }
    public DateTime DataExpiracao { get; private set; }
    public bool Concluido { get; private set; }

    private TodoEtapa(TodoEtapaId id, string descricao, DateTime dataExpiracao, bool concluido) : base(id)
    {
        Descricao = descricao;
        DataExpiracao = dataExpiracao;
        Concluido = concluido;
    }

    public static TodoEtapa Create(string descricao, DateTime dataExpiracao, bool concluido)
    {
        return new(TodoEtapaId.Create(), descricao, dataExpiracao, concluido);
    }
}