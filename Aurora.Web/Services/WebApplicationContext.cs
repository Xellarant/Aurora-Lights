using Builder.Core.Events;
using Builder.Presentation;
using Builder.Presentation.Interfaces;

namespace Aurora.Web.Services;

public sealed class WebApplicationContext : IApplicationContext
{
    public IEventAggregator EventAggregator { get; } = new Builder.Core.Events.EventAggregator();

    public AppSettingsStore Settings { get; } = new();

    public bool IsInDeveloperMode { get; set; }

    public bool EnableDiagnostics { get; set; }

    public string? LoadedCharacterFilePath { get; set; }

    public bool HasCharacterFileRequest => !string.IsNullOrWhiteSpace(LoadedCharacterFilePath);

    public void SendStatusMessage(string statusMessage)
    {
        System.Diagnostics.Debug.WriteLine($"[Aurora.Web][Status] {statusMessage}");
    }
}
