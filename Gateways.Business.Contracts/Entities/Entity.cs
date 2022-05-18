using Gateways.Business.Contracts.Contracts;

namespace Gateways.Business.Contracts.Entities;

public class Entity<T> : IEntity<T>
{
    public virtual T Id { get; set; } = default!;
}

public class Entity : Entity<string>
{
    public Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
