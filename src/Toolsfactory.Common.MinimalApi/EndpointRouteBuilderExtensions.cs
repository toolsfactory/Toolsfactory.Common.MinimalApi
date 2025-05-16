namespace Toolsfactory.Common.MinimalApi;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder builder)
    {
        var logger = builder.ServiceProvider.GetService<ILogger<IEndpointDefinition>>();

        foreach (var endpointInterface in builder.ServiceProvider.GetServices<IEndpointDefinition>())
        {
            if (endpointInterface is EndpointDefinitionBase endpoint)
            {
                var group = builder.MapGroup(endpoint.basePath);

                if (endpoint.hosts.Any())
                {
                    group = group.RequireHost(endpoint.hosts);
                }

                if (endpoint.requiresAuthorization)
                {
                    group = group.RequireAuthorization(endpoint.authorizationPolicyNames);
                }

                if (!string.IsNullOrWhiteSpace(endpoint.corsPolicyName))
                {
                    group = group.RequireCors(endpoint.corsPolicyName);
                }

                if (endpoint.includeInOpenApi)
                {
                    group.IncludeInOpenApi();
                }

                if (!string.IsNullOrWhiteSpace(endpoint.openApiDescription))
                {
                    group = group.WithDescription(endpoint.openApiDescription);
                }

                if (endpoint.metaData.Any())
                {
                    group = group.WithMetadata(endpoint.metaData);
                }

                if (!string.IsNullOrWhiteSpace(endpoint.openApiName))
                {
                    group = group.WithName(endpoint.openApiName);
                }

                if (!string.IsNullOrWhiteSpace(endpoint.openApisummary))
                {
                    group = group.WithSummary(endpoint.openApisummary);
                }

                if (!string.IsNullOrWhiteSpace(endpoint.openApiDisplayName))
                {
                    group = group.WithDisplayName(endpoint.openApiDisplayName);
                }

                if (!string.IsNullOrWhiteSpace(endpoint.openApiGroupName))
                {
                    group = group.WithGroupName(endpoint.openApiGroupName);
                }

                if (endpoint.tags.Any())
                {
                    group = group.WithTags(endpoint.tags);
                }

                if (!string.IsNullOrWhiteSpace(endpoint.cacheOutputPolicyName))
                {
                    group = group.CacheOutput(endpoint.cacheOutputPolicyName);
                }

                if (endpoint.disableRateLimiting)
                {
                    group = group.DisableRateLimiting();
                }

                if (!string.IsNullOrWhiteSpace(endpoint.rateLimitingPolicyName))
                {
                    group = group.RequireRateLimiting(endpoint.rateLimitingPolicyName);
                }

                endpoint.AddEndpoints (group);
            }
            else
            {
                endpointInterface.AddEndpoints(builder);
            }
        }
        return builder;
    }
}
