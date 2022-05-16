namespace Gateways.Business.Contracts.Entities;

public class Gateway : Entity
{
    public string Name { get; set; } = default!;
    public string IPv4 { get; set; } = default!;
}
