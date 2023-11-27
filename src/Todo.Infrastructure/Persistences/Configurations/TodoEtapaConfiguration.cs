using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Todo.Domain.Todos.Entities;
using Todo.Domain.Todos.ValueObjects;

namespace Todo.Infrastructure.Persistence.Configurations;

public sealed class TodoEtapaConfiguration : IEntityTypeConfiguration<TodoEtapa>
{
    public void Configure(EntityTypeBuilder<TodoEtapa> builder)
    {
        builder.ToTable("tb_todo_etapas");

        builder.HasKey(te => te.Id);
        builder.Property(te => te.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => TodoEtapaId.Create(value));

        builder.Property(te => te.DataExpiracao);

        builder.Property(te => te.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(te => te.Concluido);
    }
}