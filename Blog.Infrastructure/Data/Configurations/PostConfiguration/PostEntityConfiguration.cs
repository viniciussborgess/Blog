using Blog.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations.PostConfiguration;

internal class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Id)
            .ValueGeneratedNever();

        builder.Property(_ => _.Titulo)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false);

        builder.Property(_ => _.Texto)
            .IsRequired()
            .IsUnicode(false);

        builder.Property(_ => _.DataPostagem)
            .IsRequired();

        builder.HasOne(_ => _.Usuario)
            .WithMany(u => u.Posts)
            .HasForeignKey(_ => _.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
