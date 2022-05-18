using Gateways.Business.Contracts;
using System.Collections;
using System.Linq.Expressions;

namespace Gateways.Data.Implementations.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public BaseRepository(IObjectContext context)
    {
        Context = context;
        Expression = context.Query<TEntity>().Expression;
        Provider = context.Query<TEntity>().Provider;
    }

    public IObjectContext Context { get; }

    public Expression Expression { get; }
    
    public IQueryProvider Provider { get; }
    
    public Type ElementType => typeof(TEntity);

    public void Add(TEntity entity) => Context.Add(entity);

    public void Commit() => Context.Commit();

    public Task CommitAsync() => Context.CommitAsync();

    public virtual void Dispose() { }

    public IEnumerator<TEntity> GetEnumerator() => Context.Query<TEntity>().GetEnumerator();

    public void Remove(TEntity entity) => Context.Remove(entity);

    public void Update(TEntity entity) => Context.Update(entity);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
