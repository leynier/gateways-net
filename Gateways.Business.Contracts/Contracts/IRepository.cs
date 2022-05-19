namespace Gateways.Business.Contracts;

public interface IRepository<TEntity> where TEntity : class
{
    IObjectContext Context { get; }
    IQueryable<TEntity> Query();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void Commit();
    Task CommitAsync();
}
