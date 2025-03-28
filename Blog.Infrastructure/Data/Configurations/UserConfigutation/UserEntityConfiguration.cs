using Blog.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations.UserConfigutation;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Nome)
            .IsRequired()
            .IsUnicode(false);

        builder.Property(_ => _.Senha)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false);

        builder.HasMany(u => u.Posts)
            .WithOne(p => p.Usuario)
            .HasForeignKey(p => p.UsuarioId);


    }
}
