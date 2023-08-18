using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using System;
using System.Threading.RateLimiting;

namespace PublicAffairsPortal.WebUI.RateLimiters
{
    public static class ConfigureRateLimiters
    {
        public static void AddRateLimiters(WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<Microsoft.AspNetCore.Http.HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            QueueLimit = 5,
                            Window = TimeSpan.FromSeconds(10)
                        }));
            });

            
            builder.Services.AddRateLimiter(options =>
            {
                options.AddTokenBucketLimiter("Token", c =>
                {
                    c.TokenLimit = 5;
                    c.QueueLimit = 0;
                    c.TokensPerPeriod = 2;
                    c.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                });
            });
        }
    }
}
