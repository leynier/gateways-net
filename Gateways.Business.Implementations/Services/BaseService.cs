using Gateways.Business.Contracts;
using Gateways.Business.Contracts.Services;
using System.Collections;
using System.Linq.Expressions;

namespace Gateways.Business.Implementations.Services;

public class BaseService<TEntity> : IService<TEntity> where TEntity : class
{
    public BaseService(IRepository<TEntity> repository)
    {
        BaseRepository = repository;
    }

    protected IRepository<TEntity> BaseRepository { get; }

    public Type ElementType => BaseRepository.ElementType;

    public Expression Expression => BaseRepository.Expression;

    public IQueryProvider Provider => BaseRepository.Provider;

    public IEnumerator<TEntity> GetEnumerator() => BaseRepository.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(TEntity entity) => BaseRepository.Add(entity);

    public void Update(TEntity entity) => BaseRepository.Update(entity);

    public void Remove(TEntity entity) => BaseRepository.Remove(entity);

    public void Commit() => BaseRepository.Commit();

    public Task CommitAsync() => BaseRepository.CommitAsync();
}
