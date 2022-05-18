using Gateways.Common.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Gateways.Common.Models;

public class PaginationQueryModel
{
    [Min(0)]
    [FromQuery(Name = "page")]
    public int Page { get; set; } = default;
    [Min(1)]
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 10;
}
