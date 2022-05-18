namespace Gateways.Common.Helpers;

public static class GetSchema
{
    public static string GetSchemaId(Type type)
    {
        var preffix = type.Name;
        var suffix = string.Empty;
        if (preffix.EndsWith("`1"))
            preffix = preffix[..^2];
        if (type.IsGenericType)
            suffix = $"[{string.Join(",", type.GenericTypeArguments.Select(GetSchemaId))}]";
        return $"{preffix}{suffix}";
    }
}
