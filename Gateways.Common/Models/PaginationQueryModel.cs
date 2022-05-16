using Gateways.Common.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Gateways.Common.Models;

public class PaginationQueryModel<T>
{
    [FromQuery(Name = "key")]
    public T? Key { get; set; } = default;
    [Min(1)]
    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = 10;
}
