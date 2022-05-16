using Gateways.Business.Contracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace Gateways.Business.Contracts.Entities;

public class Device : Entity<int>
{
    [Required]
    public string Vendor { get; set; } = default!;
    [Required]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    [Required]
    public DeviceStatus Status { get; set; } = DeviceStatus.Offline;

    [Required]
    public string GatewayId { get; set; } = default!;
    public Gateway Gateway { get; set; } = default!;
}
