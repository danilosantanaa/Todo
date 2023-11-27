using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Todo.Domain.Menus;
using Todo.Domain.Todos.ValueObjects;

using todoDomain = Todo.Domain.Todos;

namespace Todo.Infrastructure.Persistence.Configurations;

public sealed class TodoConfiguration : IEntityTypeConfiguration<todoDomain.Todo>
{
    public void Configure(EntityTypeBuilder<todoDomain.Todo> builder)
    {
        builder.ToTable("tb_todos");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(id =>
                id.Value,
                value => TodoId.Create(value)
            );

        builder.Property(t => t.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Tipo)
            .IsRequired();

        builder.Property(t => t.RepeticaoTipo)
            .IsRequired();

        builder.Property(t => t.DataConclusao);

        builder.Property(t => t.Status);

        builder.Property(t => t.DataHoraLembrar);

        builder.HasOne<Menu>()
            .WithMany()
            .HasForeignKey(t => t.MenuId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.TodoEtapas)
            .WithOne()
            .HasForeignKey(te => te.TodoId);
    }
}