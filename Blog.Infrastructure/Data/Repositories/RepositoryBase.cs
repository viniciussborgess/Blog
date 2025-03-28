using Blog.Domain.Data.Core;
using Blog.Domain.Data.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace Blog.Infrastructure.Data.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : Entity<TKey>
{
    private readonly ApplicationDataContext _context;
    protected DbSet<TEntity> _entity;
    public RepositoryBase(ApplicationDataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _entity = _context.Set<TEntity>();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Create(TEntity entity)
    {
        _entity.Add(entity);
    }

    public TEntity Get(TKey key)
    {
        return _entity.Find(key);
    }

    public void Remove(TEntity entity)
    {
        _entity.Remove(entity);
    }

    public virtual List<TEntity> GetAll()
    {
        return _entity.ToList();
    }

    public void Update(TEntity entity)
    {
        _entity.Update(entity);
    }

}
