using Gateways.Business.Contracts.Entities;

namespace Gateways.Business.Contracts;

public interface IObjectContext : IDisposable
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    void Add<TEntity>(TEntity entity) where TEntity : class;
    void Update<TEntity>(TEntity entity) where TEntity : class;
    void Remove<TEntity>(TEntity entity) where TEntity : class;
    void Commit();
    Task CommitAsync();
}
