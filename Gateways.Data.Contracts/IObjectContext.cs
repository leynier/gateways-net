namespace Gateways.Data.Contracts;

public interface IObjectContext : IDisposable
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;
}
