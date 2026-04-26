namespace Aurora.App.Services;

/// <summary>
/// Cross-platform "Save As…" dialog. Returns the chosen full path, or null when the
/// user cancels. Writing the bytes is the caller's responsibility so we don't have
/// to marshal large PDF buffers through platform-specific APIs.
/// </summary>
public static class FileSaveService
{
    public sealed record FileTypeChoice(string Label, string Extension);

    /// <summary>
    /// Prompts the user for a save location. <paramref name="suggestedFileName"/> should
    /// include the extension. Returns the chosen full path, or null if cancelled.
    /// </summary>
    public static Task<string?> PickSaveLocationAsync(
        string suggestedFileName,
        IReadOnlyList<FileTypeChoice> fileTypes,
        string? initialDirectory = null)
    {
#if WINDOWS
        return PickSaveLocationWindowsAsync(suggestedFileName, fileTypes, initialDirectory);
#elif MACCATALYST
        return PickSaveLocationMacAsync(suggestedFileName, fileTypes, initialDirectory);
#else
        return Task.FromResult<string?>(null);
#endif
    }

#if WINDOWS
    private static async Task<string?> PickSaveLocationWindowsAsync(
        string suggestedFileName,
        IReadOnlyList<FileTypeChoice> fileTypes,
        string? initialDirectory)
    {
        var picker = new Windows.Storage.Pickers.FileSavePicker
        {
            SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary,
            SuggestedFileName      = Path.GetFileNameWithoutExtension(suggestedFileName),
        };
        foreach (var ft in fileTypes)
            picker.FileTypeChoices.Add(ft.Label, new List<string> { ft.Extension });

        // Unpackaged Win32 requires associating the picker with the app window.
        var window = Microsoft.Maui.Controls.Application.Current?.Windows.FirstOrDefault()?.Handler?.PlatformView
                     as Microsoft.UI.Xaml.Window;
        if (window != null)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
        }

        // FileSavePicker in unpackaged apps doesn't accept an explicit starting folder path;
        // the OS remembers the last location per-extension. SuggestedFileName is the best hint.
        _ = initialDirectory;

        var file = await picker.PickSaveFileAsync();
        return file?.Path;
    }
#endif

#if MACCATALYST
    private static Task<string?> PickSaveLocationMacAsync(
        string suggestedFileName,
        IReadOnlyList<FileTypeChoice> fileTypes,
        string? initialDirectory)
    {
        // NSSavePanel is an AppKit type not included in the Mac Catalyst managed
        // binding. Save directly to the requested directory (or Documents) instead.
        string dir = !string.IsNullOrEmpty(initialDirectory) && Directory.Exists(initialDirectory)
            ? initialDirectory
            : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        return Task.FromResult<string?>(Path.Combine(dir, suggestedFileName));
    }
#endif
}
