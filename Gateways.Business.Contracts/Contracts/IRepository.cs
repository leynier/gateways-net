using Gateways.Business.Contracts.Entities;

namespace Gateways.Business.Contracts;

public interface IRepository<TEntity> : IQueryable<TEntity>, IDisposable where TEntity : class
{
    IObjectContext Context { get; }
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void Commit();
    Task CommitAsync();
}
