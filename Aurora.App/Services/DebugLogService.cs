namespace Aurora.App.Services;

public enum LogLevel { Info, Warning, Error }

public sealed record LogEntry(
    DateTimeOffset Timestamp,
    LogLevel Level,
    string Message,
    string? Details = null);

/// <summary>
/// In-memory log sink. Pages and services call LogException / Log to capture runtime errors.
/// Accessible via DI injection or the static <see cref="Instance"/> for non-injected code.
/// </summary>
public sealed class DebugLogService
{
    /// <summary>Static accessor set by MauiProgram so any catch block can log without DI.</summary>
    public static DebugLogService Instance { get; internal set; } = new();

    private const int MaxEntries = 500;
    private readonly List<LogEntry> _entries = [];

    public IReadOnlyList<LogEntry> Entries => _entries;

    /// <summary>Fires on the UI thread after an entry is added or the log is cleared.</summary>
    public event Action? Changed;

    public void Log(LogLevel level, string message, string? details = null)
    {
        var entry = new LogEntry(DateTimeOffset.Now, level, message, details);
        lock (_entries)
        {
            _entries.Add(entry);
            if (_entries.Count > MaxEntries)
                _entries.RemoveAt(0);
        }
        Changed?.Invoke();
    }

    public void LogException(Exception ex, string context = "")
    {
        var message = string.IsNullOrEmpty(context)
            ? $"{ex.GetType().Name}: {ex.Message}"
            : $"{context} — {ex.GetType().Name}: {ex.Message}";

        Log(LogLevel.Error, message, ex.ToString());
    }

    public void Info(string message, string? details = null)  => Log(LogLevel.Info,    message, details);
    public void Warn(string message, string? details = null)  => Log(LogLevel.Warning, message, details);
    public void Error(string message, string? details = null) => Log(LogLevel.Error,   message, details);

    /// <summary>
    /// Logs the exception and returns its message — useful in one-liner catch blocks:
    /// <c>catch (Exception ex) { return DebugLogService.Catch(ex, "context"); }</c>
    /// </summary>
    public static string Catch(Exception ex, string context = "")
    {
        Instance.LogException(ex, context);
        return ex.Message;
    }

    /// <summary>
    /// Returns a thread-safe snapshot of current log entries.
    /// Takes the lock to avoid a race between background log calls and UI reads.
    /// </summary>
    public IReadOnlyList<LogEntry> GetSnapshot()
    {
        lock (_entries) return [.. _entries];
    }

    public void Clear()
    {
        lock (_entries) _entries.Clear();
        Changed?.Invoke();
    }
}
