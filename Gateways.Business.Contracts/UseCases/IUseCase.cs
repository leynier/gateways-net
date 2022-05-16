namespace Gateways.Business.Contracts.UseCases;

public interface IUseCase<TEntity> : IQueryable<TEntity>, IDisposable where TEntity : class
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void Commit();
    Task CommitAsync();
}
