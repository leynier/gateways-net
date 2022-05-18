namespace Gateways.Business.Contracts.Contracts;

public interface IEntity<T>
{
    T Id { get; set; }
}
