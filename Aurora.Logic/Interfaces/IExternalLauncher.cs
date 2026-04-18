namespace Builder.Presentation.Interfaces;

/// <summary>
/// Cross-platform contract for opening a file, directory, or URI with the host platform.
/// Registered via <see cref="ExternalLauncherContext"/>.
/// </summary>
public interface IExternalLauncher
{
    bool OpenPath(string path);
    bool OpenUri(string uri);
}
