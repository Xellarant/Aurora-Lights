using Builder.Presentation.Interfaces;

namespace Builder.Presentation;

/// <summary>
/// Static bridge that lets Aurora.Logic code resolve the application singleton
/// without taking a compile-time dependency on WPF.
///
/// Usage:
///   1. Aurora.Logic code calls ApplicationContext.Current.SomeProperty
///   2. The WPF App.xaml.cs (or ApplicationManager constructor) calls
///      ApplicationContext.SetCurrent(this) to wire it up at startup.
/// </summary>
public static class ApplicationContext
{
    private static IApplicationContext? _current;

    public static IApplicationContext Current =>
        _current ?? throw new InvalidOperationException(
            "ApplicationContext.Current has not been initialised. " +
            "Call ApplicationContext.SetCurrent(manager) from App.xaml.cs before using it.");

    public static void SetCurrent(IApplicationContext context) =>
        _current = context ?? throw new ArgumentNullException(nameof(context));
}
