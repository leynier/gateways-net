using Gateways.Business.Contracts;

namespace Gateways.Data.Implementations.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public BaseRepository(IObjectContext context)
    {
        Context = context;
    }

    public IObjectContext Context { get; }

    public IQueryable<TEntity> Query() => Context.Query<TEntity>();

    public void Add(TEntity entity) => Context.Add(entity);

    public void Update(TEntity entity) => Context.Update(entity);

    public void Remove(TEntity entity) => Context.Remove(entity);

    public void Commit() => Context.Commit();

    public Task CommitAsync() => Context.CommitAsync();
}
