namespace Toolsfactory.Common.MinimalApi
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;


    public abstract class EndpointDefinitionBase : IEndpointDefinition
    {
        internal string[] hosts = Array.Empty<string>();

        internal string corsPolicyName = string.Empty;

        internal string openApiDescription = string.Empty;

        internal object[] metaData = Array.Empty<object>();

        internal string openApiName = string.Empty;

        internal string openApisummary = string.Empty;

        internal string openApiDisplayName = string.Empty;

        internal string openApiGroupName = string.Empty;

        internal string[] tags = Array.Empty<string>();

        internal bool includeInOpenApi = false;

        internal bool requiresAuthorization = false;

        internal string[] authorizationPolicyNames = Array.Empty<string>();

        internal string cacheOutputPolicyName = string.Empty;

        internal readonly string basePath = "/";

        internal bool disableRateLimiting = false;

        internal string rateLimitingPolicyName = string.Empty;

        /// <summary>
        /// Initializes a new instance of <see cref="CarterModule"/>
        /// </summary>
        protected EndpointDefinitionBase() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CarterModule"/>
        /// </summary>
        /// <param name="basePath">A base path to group routes in your <see cref="CarterModule"/></param>
        protected EndpointDefinitionBase(string basePath)
        {
            this.basePath = basePath;
        }

        /// <summary>
        /// Add authorization to all routes
        /// </summary>
        /// <param name="policyNames">
        /// A collection of policy names.
        /// If <c>null</c> or empty, the default authorization policy will be used.
        /// </param>
        /// <returns></returns>
        public EndpointDefinitionBase RequireAuthorization(params string[] policyNames)
        {
            requiresAuthorization = true;
            authorizationPolicyNames = policyNames;
            return this;
        }

        /// <summary>
        /// Requires that endpoints match one of the specified hosts during routing.
        /// </summary>
        /// <param name="hosts">The hosts used during routing</param>
        /// <returns></returns>
        public EndpointDefinitionBase RequireHost(params string[] hosts)
        {
            this.hosts = hosts;
            return this;
        }

        /// <summary>
        /// Adds a CORS policy with the specified name to the module's routes.
        /// </summary>
        /// <param name="policyName">The CORS policy name</param>
        /// <returns></returns>
        public EndpointDefinitionBase RequireCors(string policyName)
        {
            corsPolicyName = policyName;
            return this;
        }

        /// <summary>
        ///  Adds <see cref="IEndpointDescriptionMetadata"/> to <see cref="EndpointBuilder.Metadata"/> 
        /// </summary>
        /// <param name="description">The description value</param>
        /// <returns></returns>
        public EndpointDefinitionBase WithDescription(string description)
        {
            openApiDescription = description;
            return this;
        }

        /// <summary>
        /// Adds the <see cref="IEndpointNameMetadata"/> to the Metadata collection for all endpoints produced
        /// </summary>
        /// <param name="name">The name value</param>
        /// <returns></returns>
        public EndpointDefinitionBase WithName(string name)
        {
            openApiName = name;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="EndpointBuilder.DisplayName"/> to the provided <paramref name="displayName"/> for all routes in the module
        /// </summary>
        /// <param name="displayName">The display name value</param>
        /// <returns></returns>
        public EndpointDefinitionBase WithDisplayName(string displayName)
        {
            openApiDisplayName = displayName;
            return this;
        }

        /// <summary>
        /// Sets the <see cref="EndpointGroupNameAttribute"/> for all routes for all routes in the module
        /// </summary>
        /// <param name="groupName">The group name value</param>
        /// <returns></returns>
        public EndpointDefinitionBase WithGroupName(string groupName)
        {
            openApiGroupName = groupName;
            return this;
        }

        /// <summary>
        /// Adds <see cref="IEndpointSummaryMetadata"/> to <see cref="EndpointBuilder.Metadata"/> for routes in the module
        /// </summary>
        /// <param name="summary">The summary value</param>
        /// <returns></returns>
        public EndpointDefinitionBase WithSummary(string summary)
        {
            openApisummary = summary;
            return this;
        }

        /// <summary>
        /// Adds the provided metadata <paramref name="items"/> to <see cref="EndpointBuilder.Metadata"/> for all routes in the module
        /// </summary>
        /// <param name="items">The items to add</param>
        /// <returns></returns>
        public EndpointDefinitionBase WithMetadata(params object[] items)
        {
            metaData = items;
            return this;
        }

        /// <summary>
        /// Adds the <see cref="ITagsMetadata"/> to <see cref="EndpointBuilder.Metadata"/> for all routes in the module
        /// </summary>
        /// <param name="tags">The tags to add</param>
        /// <returns></returns>
        public EndpointDefinitionBase WithTags(params string[] tags)
        {
            this.tags = tags;
            return this;
        }

        /// <summary>
        /// Include all routes in the module to the OpenAPI output
        /// </summary>
        /// <returns></returns>
        public EndpointDefinitionBase IncludeInOpenApi()
        {
            includeInOpenApi = true;
            return this;
        }

        /// <summary>
        ///  Marks an endpoint to be cached using a named policy.
        /// </summary>
        /// <param name="policyName">The policy name value</param>
        /// <returns></returns>
        public EndpointDefinitionBase WithCacheOutput(string policyName)
        {
            cacheOutputPolicyName = policyName;
            return this;
        }

        /// <summary>
        /// Disables rate limiting on all the routes in the module
        /// </summary>
        /// <returns></returns>
        public EndpointDefinitionBase DisableRateLimiting()
        {
            disableRateLimiting = true;
            return this;
        }

        /// <summary>
        /// Adds the specified rate limiting policy to all the routes in the module
        /// </summary>
        /// <param name="policyName">The policy name value</param>
        /// <returns></returns>
        public EndpointDefinitionBase RequireRateLimiting(string policyName)
        {
            rateLimitingPolicyName = policyName;
            return this;
        }

        public abstract void AddEndpoints(IEndpointRouteBuilder app);
    }
}
