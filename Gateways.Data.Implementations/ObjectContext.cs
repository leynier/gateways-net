using Gateways.Business.Contracts;
using Gateways.Business.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Data.Implementations;

public class ObjectContext : DbContext, IObjectContext
{
    #region Constructors

    public ObjectContext(DbContextOptions<ObjectContext> options) : base(options) { }

    #endregion

    #region Methods and Properties

    IQueryable<TEntity> IObjectContext.Query<TEntity>() => Set<TEntity>();

    void IObjectContext.Add<TEntity>(TEntity entity) => Set<TEntity>().Add(entity);

    void IObjectContext.Update<TEntity>(TEntity entity) => Set<TEntity>().Update(entity);

    void IObjectContext.Remove<TEntity>(TEntity entity) => Set<TEntity>().Remove(entity);

    void IObjectContext.Commit() => SaveChanges();

    Task IObjectContext.CommitAsync() => SaveChangesAsync();

    #endregion

    #region DbSets

    public DbSet<Device> Devices { get; set; } = default!;
    public DbSet<Gateway> Gateways { get; set; } = default!;

    #endregion
}
