using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Todo.Domain.Menus;
using Todo.Domain.Menus.ValueObjects;

namespace Todo.Infrastructure.Persistence.Configurations;

public sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("tb_menus");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuId.Create(value)
            );

        builder.Property(m => m.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.IconUrl)
            .HasMaxLength(200);
    }
}