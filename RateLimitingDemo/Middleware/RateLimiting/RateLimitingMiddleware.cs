using AspNetCoreRateLimit;

namespace RateLimitingDemo.Middleware.RateLimiting
{
    internal static class RateLimitingMiddleware
    {
        internal static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            // Used to store rate limit counters and ip rules
            services.AddMemoryCache();

            // Load in general configuration and ip rules from appsettings.json
            services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IpRateLimitingSettings").Bind(options));
            services.Configure<IpRateLimitPolicies>(options => configuration.GetSection("IpRateLimitingPolicies").Bind(options));

            // Inject Counter and Store Rules
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, CustomRateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            // Return the services
            return services;
        }

        internal static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
        {
            app.UseIpRateLimiting();
            return app;
        }
    }
}
