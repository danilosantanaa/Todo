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
        string descricao) : base(id)
    {
        Descricao = descricao;
        AddDataExpiracao();
        Concluido = false;
    }

    private TodoEtapa() { }
#pragma warning restore CS8618

    public static TodoEtapa Create(string descricao)
    {
        return new(TodoEtapaId.Create(), descricao);
    }

    public void AddDataExpiracao(DateTime dataExpiracao = default)
    {
        if (DataExpiracao < DateTime.Now)
        {
            throw new TodoEtapaDataExpiracaoInvalidoException();
        }

        DataExpiracao = dataExpiracao == DateTime.MinValue ? DateTime.Now : dataExpiracao;
    }
}