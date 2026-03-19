using Builder.Core.Events;

namespace Builder.Presentation.Interfaces;

/// <summary>
/// Cross-platform contract for the application-level singleton.
/// Implemented by ApplicationManager (WPF project) and set on startup via
/// ApplicationContext.SetCurrent(). This seam lets Aurora.Logic ViewModels
/// access the event aggregator, settings, and status without a hard WPF dep.
/// </summary>
public interface IApplicationContext
{
    IEventAggregator EventAggregator { get; }

    /// <summary>Persisted JSON settings store.</summary>
    AppSettingsStore Settings { get; }

    bool IsInDeveloperMode { get; set; }

    bool EnableDiagnostics { get; set; }

    string? LoadedCharacterFilePath { get; set; }

    bool HasCharacterFileRequest { get; }

    void SendStatusMessage(string statusMessage);
}
