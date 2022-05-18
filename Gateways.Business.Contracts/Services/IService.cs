namespace Gateways.Business.Contracts.Services;

public interface IService<TEntity> : IQueryable<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void Commit();
    Task CommitAsync();
}
