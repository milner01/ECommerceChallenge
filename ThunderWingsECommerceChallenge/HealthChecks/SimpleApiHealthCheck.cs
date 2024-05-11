using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ThunderWingsECommerceChallenge.Api.HealthChecks;

public class SimpleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (true)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("API is OK."));
        }
    }
}
