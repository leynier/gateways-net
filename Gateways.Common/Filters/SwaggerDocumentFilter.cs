using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using IDocumentFilter = Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter;

namespace Gateways.Common.Filters;

public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths;

        // Generate the new keys
        var newPaths = new Dictionary<string, OpenApiPathItem>();
        var removeKeys = new List<string>();
        foreach (var path in paths)
        {
            // ignore path parameters
            var newKey = path.Key
                .Split('/')
                .Select(p => p.StartsWith("{") && p.EndsWith("}")
                    ? p
                    : p.ToLower())
                .Aggregate((a, b) => $"{a}/{b}");
            if (newKey != path.Key)
            {
                removeKeys.Add(path.Key);
                newPaths.Add(newKey, path.Value);
            }
        }
        // Add the new keys
        foreach (var path in newPaths)
        {
            swaggerDoc.Paths.Add(path.Key, path.Value);
        }
        // Remove the old keys
        foreach (var key in removeKeys)
        {
            swaggerDoc.Paths.Remove(key);
        }
    }
}
