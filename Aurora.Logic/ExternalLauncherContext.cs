using Builder.Presentation.Interfaces;

namespace Builder.Presentation;

/// <summary>
/// Static bridge for the platform launcher.
/// Set by each client on startup so shared logic can open files, folders, and URLs
/// without a direct dependency on <c>Process.Start</c> or OS-specific APIs.
/// </summary>
public static class ExternalLauncherContext
{
    public static IExternalLauncher? Current { get; set; }
}
