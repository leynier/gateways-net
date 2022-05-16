using Gateways.Business.Contracts;
using Gateways.Business.Contracts.UseCases;
using System.Collections;
using System.Linq.Expressions;

namespace Gateways.Business.Implementations.UseCases;

public class BaseUseCase<TEntity> : IUseCase<TEntity> where TEntity : class
{
    public BaseUseCase(IRepository<TEntity> repository)
    {
        BaseRepository = repository;
    }

    protected IRepository<TEntity> BaseRepository { get; }

    public Type ElementType => BaseRepository.ElementType;

    public Expression Expression => BaseRepository.Expression;

    public IQueryProvider Provider => BaseRepository.Provider;

    public void Dispose() => BaseRepository.Dispose();

    public IEnumerator<TEntity> GetEnumerator() => BaseRepository.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(TEntity entity) => BaseRepository.Add(entity);

    public void Update(TEntity entity) => BaseRepository.Update(entity);

    public void Remove(TEntity entity) => BaseRepository.Remove(entity);

    public void Commit() => BaseRepository.Commit();

    public Task CommitAsync() => BaseRepository.CommitAsync();
}
