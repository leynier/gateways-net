namespace Gateways.Business.Contracts.Services;

public interface IService<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void Commit();
    Task CommitAsync();
}
