# Roadmap

## Near-Term Priorities

1. Stabilize the shared layer as a true multi-client core.
2. Keep the MAUI app moving forward as the primary modern client.
3. Preserve WPF compatibility without treating WPF as the main innovation surface.
4. Start a Phase 0 `Aurora.Web` host with no long-term user-content storage.

## Shared Layer

Current focus:

- continue removing platform assumptions from `Aurora.Logic`
- keep launch/dialog/file seams behind abstractions
- make shared content loading and file-path behavior safe across Windows and macOS
- add tests around save/load, build choices, equipment state, and generated output over time

## MAUI

Current focus:

- expand the modern shell and workflow coverage
- improve desktop polish
- validate Mac Catalyst behavior on actual macOS hardware
- keep MAUI-only features, such as sessions, where they provide clear value for migration

## WPF

Current position:

- continue supporting it as a parallel client
- preserve cross-compatibility for character/content data
- avoid large new feature investments unless they are compatibility-critical

## Aurora.Web Phase 0

Target model:

- hosted `core + SRD` baseline content only
- user-supplied XML content handled privately and ephemerally
- no required accounts
- no long-term persistence of uploaded non-SRD content
- export/download of `.dnd5e` files and generated PDFs

Planned implementation shape:

1. Add a web host project.
2. Introduce a shared Razor/component assembly so the MAUI and web hosts can reuse the same UI source.
3. Build a session-scoped content overlay service.
4. Parse uploaded XML once per session and build in-memory indexes for compendium/equipment/spell lookups.
5. Add temporary workspace cleanup/expiration.
6. Introduce a download-focused character/PDF flow instead of any server persistence.

## Web Phase 0 Non-Goals

- public hosting of non-SRD content packs
- cross-device user libraries
- account system or user database
- long-term server-side character storage
- whole-desktop-folder mirroring as the initial upload model

## Likely Later Web Phases

- optional accounts for persistent user libraries
- richer upload/import ergonomics
- stronger compendium/search indexing if in-memory indexing proves insufficient
- browser-safe preferences and session restore
- optional external authentication providers if and when persistent user data becomes worthwhile
