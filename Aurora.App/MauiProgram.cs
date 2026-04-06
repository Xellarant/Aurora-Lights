using Aurora.App.Services;
using Builder.Presentation;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Aurora.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Wire up all Aurora.Logic static context seams before anything else.
        var appContext = new MauiApplicationContext();
        ApplicationContext.SetCurrent(appContext);
        SelectionRuleExpanderContext.Current = new MauiSelectionRuleExpanderHandler();
        SpellcastingSectionContext.Current    = new MauiSpellcastingSectionHandler();
        MessageDialogContext.Current          = new MauiMessageDialogService();
        CharacterSheetGeneratorContext.Current = new MauiCharacterSheetGenerator();

        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();

        builder.Services.AddSingleton<MauiApplicationContext>(appContext);
        builder.Services.AddSingleton<CharacterService>();
        builder.Services.AddSingleton<CharacterTabService>();
        builder.Services.AddSingleton<UserPreferencesService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
