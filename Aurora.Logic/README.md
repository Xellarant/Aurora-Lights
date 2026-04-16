# Aurora.Logic

`Aurora.Logic` is the shared application core for Aurora-Lights.

## Purpose

This project is the compatibility and migration anchor between clients.

It is intended to contain:

- rules and progression logic
- character/domain models
- content loading and indexing
- sheet generation
- shared services and contexts
- app settings and neutral infrastructure

## Current Direction

The main architectural goal is to keep this project as client-neutral as possible so it can be reused by:

- the legacy WPF app
- the MAUI app
- a future `Aurora.Web` host

Recent cleanup in this project includes:

- removal of the `Aurora.Presentation.dll` dependency
- removal of shared `System.Drawing` usage
- replacement of direct `Process.Start(...)` usage with a launcher abstraction
- path handling fixes so shared content loading no longer assumes Windows path separators

## Important Constraint

This project still carries some historical baggage from the legacy codebase, including decompiled code and a handful of seams that were originally designed around WPF-era assumptions. Those seams are being moved behind shared abstractions incrementally rather than through one large rewrite.

## Expected Responsibility Split

- shared runtime/state repair belongs here when both clients benefit from a canonical behavior
- host-only conveniences belong in the client projects
- temporary web-session upload handling may eventually live behind interfaces here, but not as browser-specific code
