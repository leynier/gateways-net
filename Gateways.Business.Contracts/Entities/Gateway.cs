using System.ComponentModel.DataAnnotations;

namespace Gateways.Business.Contracts.Entities;

public class Gateway : Entity
{
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public string IPv4 { get; set; } = default!;
    public IEnumerable<Device> Devices { get; set; } = new List<Device>();
}
