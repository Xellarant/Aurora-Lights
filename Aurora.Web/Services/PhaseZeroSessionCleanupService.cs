using Microsoft.Extensions.Options;

namespace Aurora.Web.Services;

public sealed class PhaseZeroSessionCleanupService : BackgroundService
{
    private readonly IWebHostEnvironment _environment;
    private readonly PhaseZeroSessionOptions _options;
    private readonly ILogger<PhaseZeroSessionCleanupService> _logger;

    public PhaseZeroSessionCleanupService(
        IWebHostEnvironment environment,
        IOptions<PhaseZeroSessionOptions> options,
        ILogger<PhaseZeroSessionCleanupService> logger)
    {
        _environment = environment;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                CleanupExpiredWorkspaces();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Phase 0 session cleanup encountered an error.");
            }

            TimeSpan delay = TimeSpan.FromMinutes(Math.Max(1, _options.CleanupIntervalMinutes));
            await Task.Delay(delay, stoppingToken);
        }
    }

    private void CleanupExpiredWorkspaces()
    {
        string root = ResolveRootDirectory();
        if (!Directory.Exists(root))
            return;

        DateTime cutoff = DateTime.UtcNow.AddMinutes(-Math.Max(1, _options.IdleExpirationMinutes));
        foreach (string directory in Directory.GetDirectories(root))
        {
            DateTime lastWrite = Directory.GetLastWriteTimeUtc(directory);
            if (lastWrite >= cutoff)
                continue;

            try
            {
                Directory.Delete(directory, recursive: true);
                _logger.LogInformation("Deleted expired phase 0 session workspace {WorkspacePath}", directory);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to delete expired phase 0 workspace {WorkspacePath}", directory);
            }
        }
    }

    private string ResolveRootDirectory()
    {
        if (!string.IsNullOrWhiteSpace(_options.RootDirectory))
            return Path.GetFullPath(_options.RootDirectory);

        return Path.Combine(_environment.ContentRootPath, "App_Data", "phase0-sessions");
    }
}
