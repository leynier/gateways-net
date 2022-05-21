using AutoMapper;

namespace Gateways.Api.Tests;

public static class AutoMapperFactory
{
    public static IMapper CreateMapper()
    {
        var profiles = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.Namespace == "Gateways.Api.MapperProfiles")
            .Where(x => !x.ContainsGenericParameters)
            .Where(x => x.IsClass)
            .Where(x => !x.IsAbstract)
            .Where(x => x.IsAssignableTo(typeof(Profile)))
            .ToList();
        return new MapperConfiguration(config =>
        {
            foreach (var profile in profiles)
                config.AddProfile(profile);
        }).CreateMapper();
    }
}
