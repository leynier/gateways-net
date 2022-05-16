using Gateways.Business.Contracts;
using Gateways.Business.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Data.Implementations;

public class ObjectContext : DbContext, IObjectContext
{
    public ObjectContext(DbContextOptions<ObjectContext> options) : base(options) { }

    IQueryable<TEntity> IObjectContext.Query<TEntity>() => Set<TEntity>();

    void IObjectContext.Add<TEntity>(TEntity entity) => Set<TEntity>().Add(entity);

    void IObjectContext.Update<TEntity>(TEntity entity) => Set<TEntity>().Update(entity);

    void IObjectContext.Remove<TEntity>(TEntity entity) => Set<TEntity>().Remove(entity);

    void IObjectContext.Commit() => SaveChanges();

    Task IObjectContext.CommitAsync() => SaveChangesAsync();

    public DbSet<Gateway> Gateways { get; set; } = default!;
}
