# Aurora-Lights

Aurora-Lights is a modernization effort for the Aurora character builder codebase.

The long-term direction is:

- keep the legacy WPF app available as a parallel client
- continue evolving the MAUI app as the primary modern desktop experience
- move shared rules, models, and content handling into `Aurora.Logic`
- prepare the codebase for an eventual browser-hosted `Aurora.Web`

This repository includes decompiled legacy code, new cross-platform work, and in-progress migration seams. It is intentionally transitional.

## Projects

- `Aurora.Lights.csproj`
  Legacy WPF desktop client. Still supported as a parallel client and expected to remain compatible with the same core data formats.

- `Aurora.App`
  MAUI Blazor Hybrid client. This is the active modernization target and now supports Windows and Mac Catalyst build targets.

- `Aurora.Web`
  ASP.NET Core Blazor web host for the browser experience. Phase 0 currently focuses on anonymous, session-scoped uploads over embedded core + SRD content.

- `Aurora.Logic`
  Shared logic layer consumed by both clients. This project contains domain models, services, content loading, sheet generation, and most of the migration target for client-neutral behavior.

- `tools/ExtractResources`
  Utility project for working with extracted content/resources.

## Current Direction

Recent work has focused on making the shared layer less desktop-specific:

- removed the old `Aurora.Presentation.dll` dependency from `Aurora.Logic`
- moved selected character-load compatibility behavior into shared services
- removed shared `System.Drawing` usage in favor of stream-based resource copying
- introduced a shared external launcher abstraction instead of direct `Process.Start(...)`
- widened `Aurora.App` to include `net10.0-maccatalyst`
- cleaned up path handling in shared data-loading code so it does not assume Windows path separators

The MAUI app is still the most actively changing client and contains newer features, including session tracking and a more modern shell experience.

## Web Roadmap

The next major track is an `Aurora.Web` host.

The current planned first release is a Phase 0 web model:

- hosted baseline content is limited to embedded core + SRD data
- users can upload private XML content for the current session
- no server-side long-term persistence of uploaded non-SRD content
- character files and generated PDFs are downloaded back to the user
- session-scoped uploaded content is parsed once and exposed through in-memory indexes

See [docs/ROADMAP.md](docs/ROADMAP.md) for the fuller near-term roadmap.

## Documentation

- [Aurora.App/README.md](Aurora.App/README.md)
- [Aurora.Logic/README.md](Aurora.Logic/README.md)
- [docs/ROADMAP.md](docs/ROADMAP.md)

## Build Notes

Useful project-level build commands:

```powershell
dotnet build .\Aurora.Logic\Aurora.Logic.csproj -v minimal
dotnet build .\Aurora.App\Aurora.App.csproj -v minimal
dotnet build .\Aurora.Web\Aurora.Web.csproj -v minimal
dotnet build .\Aurora.Lights\Aurora.Lights.csproj -v minimal
```

In this environment, project builds are the most reliable signal. A plain repository-root `dotnet build` has intermittently failed early without surfacing diagnostics even when the individual projects build successfully.
