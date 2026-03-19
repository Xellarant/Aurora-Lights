using Builder.Presentation.Interfaces;

namespace Builder.Presentation;

/// <summary>
/// Static bridge for the message/dialog service.
/// Set by the platform-specific implementation on startup so that
/// Aurora.Logic code can surface errors without a WPF dependency.
/// If unset, messages are silently dropped (logged via Builder.Core.Logging).
/// </summary>
public static class MessageDialogContext
{
    public static IMessageDialogService? Current { get; set; }
}
