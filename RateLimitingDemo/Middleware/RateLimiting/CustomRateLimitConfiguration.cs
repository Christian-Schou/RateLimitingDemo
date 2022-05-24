using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace RateLimitingDemo.Middleware.RateLimiting
{
    internal class CustomRateLimitConfiguration : RateLimitConfiguration
    {
        public CustomRateLimitConfiguration(
            IOptions<IpRateLimitOptions> ipOptions, 
            IOptions<ClientRateLimitOptions> clientOptions) : base(ipOptions, clientOptions)
        {
        }

        public override void RegisterResolvers()
        {
            ClientResolvers.Add(new ClientIdResolverContributor());
        }
    }

    internal class ClientIdResolverContributor : IClientResolveContributor
    {
        public Task<string> ResolveClientAsync(HttpContext httpContext)
        {
            return Task.FromResult<string>(httpContext.Request.Query["CustomKey"]);
        }
    }
}