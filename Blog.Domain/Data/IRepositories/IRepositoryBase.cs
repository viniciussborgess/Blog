using Blog.Domain.Data.Core;

namespace Blog.Domain.Data.IRepositories;

public interface IRepositoryBase<TEntity, Tkey> where TEntity : Entity<Tkey>
{
    TEntity Get(Tkey key);
    void Create(TEntity entity);
    Task<bool> SaveChangesAsync();
    List<TEntity> GetAll();
    void Remove(TEntity entity);
    void Update(TEntity entity);
}
