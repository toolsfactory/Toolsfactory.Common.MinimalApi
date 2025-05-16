namespace Toolsfactory.Common.MinimalApi;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEndpointModules(this IServiceCollection services)
    {

        return AddEndpointModules(services, _ => { });
    }

    public static IServiceCollection AddEndpointModules(this IServiceCollection services, Action<FeatureConfigurator> configure)
    {
        var configurator = new FeatureConfigurator();
        configure(configurator);

        var candidateTypes = configurator.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t =>
                !t.IsAbstract &&
                !t.IsInterface &&
                (typeof(IEndpointDefinition).IsAssignableFrom(t) ||
                 typeof(EndpointDefinitionBase).IsAssignableFrom(t)) &&
                !configurator.IsExcluded(t));

        foreach (var type in candidateTypes)
        {
            services.TryAddEnumerable(
                new ServiceDescriptor(typeof(IEndpointDefinition), type, ServiceLifetime.Singleton));
        }

        return services;
    }
}