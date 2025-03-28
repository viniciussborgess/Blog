using Blog.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Blog.Infrastructure;

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDataContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
