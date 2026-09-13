namespace BimmerSpot.Services;

public class FeatureFlagService : IFeatureFlagService
{
    private IConfiguration _configuration;

    public FeatureFlagService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool AllowLocalAccountCreation() =>
        _configuration.GetValue<bool>("FeatureFlags:AllowLocalAccountCreation");

}
