namespace Gateways.Common.Models;

public class Paginated<T>
{
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
}
