========================================================================
AGENT-README: CodeBrix.Platform.Fonts.NotoMusic
A Comprehensive Guide for AI Coding Agents
========================================================================


OVERVIEW
========================================================================

CodeBrix.Platform.Fonts.NotoMusic is a .NET 10 redistribution of the
Noto Music font, packaged for the CodeBrix family. It supplies the Noto
Music font as a build-time content asset for CodeBrix.Platform-forked
applications, and is equally usable as a plain content-files NuGet in
any .NET 10 project.

Noto Music is a MUSICAL-NOTATION SYMBOLS font, not a text face. It
covers the Unicode music blocks — Western musical symbols (U+1D100..
U+1D1FF), Byzantine musical symbols (U+1D000..U+1D0FF) and ancient
Greek musical notation (U+1D200..U+1D24F) — plus the miscellaneous-
symbols music characters (U+2669..U+266F) and a small set of supporting
Latin, Greek and Cyrillic characters. It is meant to be referenced
ALONGSIDE one of the family's text font packages (OpenSans, Roboto,
RobotoMono, Merriweather, Fluent), never instead of one.

The package is structurally the family's simplest font package, because
Noto Music publishes exactly ONE face upstream: Regular weight, upright,
Normal stretch, static (no variable font, no italics). Where the sibling
packages ship a variable font plus a set of static instances per family,
this package ships one `.ttf` and a one-entry manifest.

The library has effectively no managed code: the assembly is a metadata-
only .NET 10 DLL whose sole purpose is to host the bundled font content
file. The interesting payload lives in:

  - 1 `.ttf` font file (`NotoMusic.ttf`) under
    lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/ inside the nupkg.
  - 1 `.ttf.manifest` JSON file whose single entry maps the
    Normal/400/Normal font_style/font_weight/font_stretch triple to the
    font file.
  - A `.uprimarker` file that CodeBrix.Platform build pipelines use to
    discover UPRI-bearing font asset packages.

Two deliberate STRUCTURAL OMISSIONS relative to the sibling packages:

  1. NO buildTransitive `.targets` file. The sibling packages use it to
     prune their dash-bearing static instances at consumer-build time on
     platforms without manifest support. This package's only font is
     dash-free and must always be present, so there is nothing to prune
     and no `.targets` file ships. Do not add one.
  2. NO `CODEBRIX-DEVELOP.json` descriptor. Noto Music is not offered as
     an application text font in CodeBrix.Develop's "New CodeBrix.Platform
     Application" experience — it is a symbols companion, not a text face
     — so the package deliberately carries no descriptor. Do not add one.


INSTALLATION
========================================================================

NuGet package: CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever

  dotnet add package CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever

The library namespace inside the assembly is
`CodeBrix.Platform.Fonts.NotoMusic` (without the `.OflLicenseForever`
suffix; that suffix exists only on the NuGet PackageId for
license-disambiguation across the CodeBrix family).

Target framework: .NET 10.0 or higher.


KEY NAMESPACE
========================================================================

The library exposes no public managed types in its first iteration — the
assembly is metadata-only. Consumers reference the bundled font content
file via its `ms-appx:///` URI rooted at the assembly content folder:

  ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf

Do NOT append a `#FamilyName` fragment to this URI. CodeBrix.Platform
strips the fragment before resolving the font, so it buys nothing — and
on the value assigned to `FeatureConfiguration.Font.DefaultTextFontFamily`
it actively breaks the startup font-manifest preload, because the
".manifest" suffix the preload appends lands inside the URI fragment and
is then dropped. (Setting Noto Music as the default TEXT font would be a
mistake anyway — it is a symbols font.)


FONT INVENTORY
========================================================================

The package ships 1 `.ttf` file plus 1 `.ttf.manifest` file.

  NotoMusic.ttf — the single upstream face (Regular, upright, Normal
                  stretch, static). Renamed, byte-for-byte, from the
                  upstream file `NotoMusic-Regular.ttf`. Version 2.003.

  NotoMusic.ttf.manifest — one entry: Normal / 400 / Normal ->
                  ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf

THE NO-VARIABLE-FONT QUIRK (same shape as Iosevka in RobotoMono)
------------------------------------------------------------------------

Noto Music publishes no variable font, so the dash-free `NotoMusic.ttf`
(the slot the sibling packages fill with a variable font) is the static
Regular instance itself, and the manifest's weight-400 entry points at it
directly. Consequences:

  1. Requests for weights other than 400, or for italic, fall back to
     whatever synthesis the platform applies. This is a known, documented
     limitation of the upstream font — do not "fix" it by inventing
     manifest entries for weights the font does not carry.
  2. There is no `NotoMusic-Regular.ttf` in the package; tests pin both
     the rename and the single-entry manifest.


CORE API REFERENCE
========================================================================

This library has no public managed API. Consumers interact with it only
through the NuGet content path
(`ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf`) used
as a `FontFamily` value in XAML or in code that constructs XAML element
trees.

If a future iteration of this library exposes a managed API (e.g. typed
accessors that return font streams or paths for non-CodeBrix.Platform
consumers), it will live under the `CodeBrix.Platform.Fonts.NotoMusic`
root namespace and be documented in this file.


ARCHITECTURE
========================================================================

Repository layout:

  CodeBrix.Platform.Fonts.NotoMusic/
    src/CodeBrix.Platform.Fonts.NotoMusic/
      CodeBrix.Platform.Fonts.NotoMusic.csproj
      InternalsVisibleTo.cs
      CodeBrix.Platform.Fonts.NotoMusic.uprimarker  (empty file)
      Fonts/
        NotoMusic.ttf
        NotoMusic.ttf.manifest
    tests/CodeBrix.Platform.Fonts.NotoMusic.Tests/
      CodeBrix.Platform.Fonts.NotoMusic.Tests.csproj
      AssemblyMetadataTests.cs
      ContentFilePresenceTests.cs
      ContentManifestTests.cs
      TestAssetPaths.cs
    AGENT-README.txt
    LICENSE            (SIL OFL 1.1; Noto Music copyright header)
    OFL.txt            (byte-identical to LICENSE; packed into the nupkg)
    README.md
    THIRD-PARTY-NOTICES.txt

Inside the produced NuGet (.nupkg), the file layout is:
  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic.dll
  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic.uprimarker
  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf
  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf.manifest
  AGENT-README.txt
  README.md
  OFL.txt
  THIRD-PARTY-NOTICES.txt
  icon-codebrix-128.png

The `lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/` content layout
is load-bearing: the `ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/...`
URI that consumers reference resolves relative to the assembly name, so if
the assembly is renamed the content folder must be renamed in lockstep.


CODING CONVENTIONS (CodeBrix family)
========================================================================

This repository follows every CodeBrix family convention. Most are
inherited from the standard library scaffold; key points:

  * Target framework: net10.0 only. No multi-targeting.
  * Nullable reference types (NRT): OFF (do not set <Nullable>enable</Nullable>).
    No `?` annotations on reference types; no `!` null-forgiveness operator.
    Value-type nullables (`int?`, `DateOnly?`, etc.) are fine.
  * No global usings.
  * `<GenerateDocumentationFile>true</GenerateDocumentationFile>` is on.
    Every public/protected member of a public type needs an XML doc
    comment. CS1591 is fixed at source, never suppressed. (In this
    library's first iteration there are no public types, so CS1591
    is trivially clean.)
  * Tests use xUnit v3 + SilverAssertions;
    `TestContext.Current.CancellationToken` is threaded through any
    cancellable call inside a test.
  * No project-level warning suppression (`<NoWarn>`, `<WarningLevel>0</>`,
    `<TreatWarningsAsErrors>false</>`, etc. are all forbidden).
  * The whole package — wrapper code and bundled font alike — is licensed
    under SIL OFL 1.1; the csproj `<PackageLicenseExpression>` is `OFL-1.1`.
    The `<Copyright>` line preserves the upstream font attribution:
      Copyright (c) 2026 Jeremy Ellis and contributors. Noto Music font
      (c) 2022 The Noto Project Authors; distributed under SIL OFL 1.1.

For the full list of family conventions see CODEBRIX_LIBRARY_OBSERVATIONS.txt
in the CodeBrix.Library.Dev-private repo.


TESTING
========================================================================

Tests live under tests/CodeBrix.Platform.Fonts.NotoMusic.Tests/. Run with:

  dotnet test CodeBrix.Platform.Fonts.NotoMusic.slnx

The test suite covers:

  * Manifest JSON: that the `.ttf.manifest` deserializes cleanly, carries
    exactly one entry, covers only weight 400 in the Normal style and
    Normal stretch, that the entry's family_name path is rooted at
    `ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/` and points at a
    file that exists on disk, that the weight-400 entry points at the
    dash-free NotoMusic.ttf, and that no foreign family token from the
    sibling packages leaked in — so the package's limitations and renames
    stay decisions rather than accidents.
  * Content-file presence: that NotoMusic.ttf and its manifest exist on
    disk next to the test assembly's expected build-output font folder
    (resolved via `AppContext.BaseDirectory` + `TestAssets/Fonts/`,
    centralized in `TestAssetPaths`), that exactly one `.ttf` ships, that
    no dash-bearing or "Regular"-named file survived the rename, and that
    the `.uprimarker` file exists and is empty.
  * Assembly metadata: that the produced library assembly is named
    `CodeBrix.Platform.Fonts.NotoMusic`, targets .NET 10, and exports no
    public types.


PROVENANCE
========================================================================

This package is not a port of any upstream packaging project. The
`.csproj`, `.ttf.manifest`, `.uprimarker`, and documentation are original
CodeBrix-family files, authored by mirroring the sibling
CodeBrix.Platform.Fonts.RobotoMono package (the most recent font package
at the time). The only third-party material is the Noto Music `.ttf` font
binary, which is redistributed bit-for-bit unmodified. Its provenance
(including the NotoMusic-Regular.ttf -> NotoMusic.ttf rename) and the SIL
OFL 1.1 terms are recorded in THIRD-PARTY-NOTICES.txt (binary `.ttf`
files cannot carry an inline provenance comment).

Font source:
  - Noto Music: Google Fonts download (single static face; upstream
                project https://github.com/notofonts/music).


KNOWN GOTCHAS
========================================================================

  * `ms-appx:///` URIs are resolved by the CodeBrix.Platform runtime, not
    by .NET itself. Outside a CodeBrix.Platform host, those URIs won't
    resolve. Plain .NET 10 console / test apps that reference this package
    can still access the .ttf file via the package's on-disk location
    (`<nuget-cache>/codebrix.platform.fonts.notomusic.ofllicenseforever/<version>/lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf`),
    but they have to do that lookup themselves.

  * Noto Music is a SYMBOLS font. Do not set it as an application's
    default text font, do not add it to CodeBrix.Develop's font choices,
    and do not expect it to render body text — its Latin/Greek/Cyrillic
    coverage exists to support notation labels, not prose. The CodeBrix
    family's no-system-font-fallback rule means codepoints outside its
    coverage render as .notdef, by design.

  * NotoMusic.ttf is NOT a variable font, and the font has exactly one
    weight and style. Do not invent manifest entries for weights the font
    does not carry, and do not add a `NotoMusic-Regular.ttf` duplicate —
    the manifest's weight-400 entry points at NotoMusic.ttf deliberately.

  * There is deliberately NO buildTransitive `.targets` file and NO
    `CODEBRIX-DEVELOP.json` in this package (see OVERVIEW). If a future
    change adds static instances (upstream has published only one face
    for years, so this is unlikely), revisit the `.targets` question then.

  * NEVER add a `#FamilyName` fragment to a font URI in this package's
    documentation. CodeBrix.Platform strips it during font resolution,
    and on `DefaultTextFontFamily` it silently disables the startup
    manifest preload (the appended ".manifest" lands inside the fragment
    and is dropped by `Uri.PathAndQuery`).

  * The Noto Music copyright statement declares no Reserved Font Name, so
    SIL OFL 1.1 condition 3 does not restrict any name used here. The
    `.ttf` binary is nonetheless redistributed unmodified; do not alter
    the font bytes. File renames are fine (and recorded in
    THIRD-PARTY-NOTICES.txt); byte edits are not.
