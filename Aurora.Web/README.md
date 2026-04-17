# Aurora.Web

`Aurora.Web` is the browser-hosted Phase 0 web shell for Aurora-Lights.

## Current Goal

This first implementation slice focuses on anonymous, session-scoped usage:

- embedded baseline content remains the long-term public host content
- users can upload private `.xml`, `.zip`, and `.dnd5e` files for the current session
- uploads are stored in a temporary workspace only
- uploaded XML is indexed into lightweight in-memory element summaries
- a read-only compendium view merges baseline content with the current session overlay
- imported character files can be opened into the current browser session
- new characters can be created inside the temporary session workspace
- active characters can be downloaded back out as `.dnd5e` files
- active characters can also be exported as a lightweight PDF summary
- stale workspaces are cleaned up automatically

## Current Scope

This project does not yet expose the full Aurora builder UI in the browser.
The current implementation is intended to land the web host, upload model, merged-content browsing, and the first create/open/export character loop so the next passes can connect full editing, richer compendium pages, and richer PDF/export flows.

## Near-Term Next Steps

- expand the merged content service beyond the first compendium page
- extract or link shared Razor UI into a reusable component assembly
- replace the lightweight PDF summary export with a fuller character-sheet renderer
- add full round-trip editing flows

## Current Constraints

- the shared Aurora runtime still relies on process-wide singletons, so `Aurora.Web` currently serializes character-engine operations behind a server-side lock
- this is acceptable for an early Phase 0 proof-of-concept, but it is not the final multi-user shape
