# Aurora.App

`Aurora.App` is the MAUI Blazor Hybrid client for Aurora-Lights.

## Purpose

This project is the modern UI shell for the application. It reuses shared rules and content logic from `Aurora.Logic`, while providing a new UI built with Razor components, MudBlazor, and MAUI hosting.

## What Lives Here

- MAUI host/bootstrap
- Blazor routing and layouts
- page components for build, equipment, manage, sheet, preferences, and session flows
- MAUI-specific service implementations for launcher, dialogs, preferences, PDF preview, and other host seams
- desktop-oriented UX features that do not need to be shared back to the WPF client

## Current Status

The app now builds for:

- `net10.0-windows10.0.19041.0`
- `net10.0-maccatalyst`

Windows remains the most exercised target today. Mac Catalyst now has a compile path and bootstrap files in place, but still needs real macOS runtime validation.

## Relationship To Other Projects

- depends on `Aurora.Logic` for shared domain behavior
- should avoid owning business rules that need to stay compatible with WPF
- may continue owning MAUI-only features, especially where they are intentionally meant to encourage migration to the newer client

Examples of MAUI-only concerns include:

- native shell/window behavior
- session-tracker UX
- MAUI preferences storage
- browser-like PDF preview hosted in a MAUI window

## Future Direction

The planned browser-hosted `Aurora.Web` should reuse the same Razor UI patterns where practical, but it should not depend directly on this MAUI project assembly. Shared Razor components will likely move into or be linked through a separate shared UI project when that work begins.
