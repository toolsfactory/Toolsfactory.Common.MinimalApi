namespace Toolsfactory.Common.MinimalApi;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;


public class FeatureConfigurator
{
    private readonly HashSet<Assembly> _assemblies = new();
    private readonly HashSet<Type> _excludedTypes = new();

    public FeatureConfigurator ScanAssembliesFrom(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            _assemblies.Add(assembly);
        }
        return this;
    }

    public FeatureConfigurator Exclude<TModule>() where TModule : IEndpointDefinition
    {
        _excludedTypes.Add(typeof(TModule));
        return this;
    }

    public FeatureConfigurator Exclude(params Type[] types)
    {
        foreach (var type in types)
        {
            _excludedTypes.Add(type);
        }
        return this;
    }

    internal IEnumerable<Assembly> GetAssemblies() => _assemblies;

    internal bool IsExcluded(Type type) => _excludedTypes.Contains(type);
}