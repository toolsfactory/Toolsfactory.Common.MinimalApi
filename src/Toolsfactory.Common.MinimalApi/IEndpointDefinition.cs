namespace Toolsfactory.Common.MinimalApi;

using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public interface IEndpointDefinition
{
    void AddEndpoints(IEndpointRouteBuilder app);
}
