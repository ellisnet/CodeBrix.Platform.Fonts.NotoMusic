========================================================================
MAINTAINER-README: CodeBrix.Platform.Fonts.NotoMusic
Notes for people and agents MAINTAINING this repository — not for
package consumers
========================================================================


PURPOSE AND SCOPE
========================================================================

This repository produces exactly one NuGet package:

  CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever
      built from src/CodeBrix.Platform.Fonts.NotoMusic/
      consumer documentation: AGENT-README.txt (repository root)

The package is a font asset carrier: a metadata-only .NET 10 assembly
plus one `.ttf` file, one `.ttf.manifest` and a `.uprimarker` marker.
There is no product source code to maintain — the maintenance surface is
the font file, the manifest and the tests that pin both.

It is the family's simplest font package: no buildTransitive `.targets`
file and no CODEBRIX-DEVELOP.json descriptor (see NOTES for why, and for
what would have to change before adding either).

If you are consuming the package rather than changing this repository,
read AGENT-README.txt instead and stop here.


REPOSITORY LAYOUT
========================================================================

  CodeBrix.Platform.Fonts.NotoMusic/
    CodeBrix.Platform.Fonts.NotoMusic.slnx
    AGENT-README.txt            (consumer docs; packed into the nupkg)
    MAINTAINER-README.txt       (this file; NOT packed)
    EXTRAS-README.txt           (NOT packed)
    README-INDEX.txt            (NOT packed)
    README.md                   (GitHub + nuget.org; packed)
    LICENSE                     (SIL OFL 1.1; Noto Music copyright header)
    OFL.txt                     (byte-identical to LICENSE; packed)
    THIRD-PARTY-NOTICES.txt     (packed)
    icon-codebrix-128.png       (packed)
    src/CodeBrix.Platform.Fonts.NotoMusic/
      CodeBrix.Platform.Fonts.NotoMusic.csproj
      InternalsVisibleTo.cs
      CodeBrix.Platform.Fonts.NotoMusic.uprimarker      (empty file)
      Fonts/
        NotoMusic.ttf
        NotoMusic.ttf.manifest
    tests/CodeBrix.Platform.Fonts.NotoMusic.Tests/
      CodeBrix.Platform.Fonts.NotoMusic.Tests.csproj
      AssemblyMetadataTests.cs
      ContentFilePresenceTests.cs
      ContentManifestTests.cs
      TestAssetPaths.cs

The `.slnx` carries the two projects plus a "Solution Items" folder
listing AGENT-README.txt, icon-codebrix-128.png, LICENSE, OFL.txt,
README.md and THIRD-PARTY-NOTICES.txt.

The `lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/` layout inside
the nupkg is load-bearing: the `ms-appx:///` URI consumers reference
resolves relative to the assembly name, so if the assembly is ever
renamed, the packed content folder must be renamed in lockstep and the
manifest URI rewritten.


BUILDING
========================================================================

  dotnet build CodeBrix.Platform.Fonts.NotoMusic.slnx

The library csproj sets `GeneratePackageOnBuild=true`, so an ordinary
build also produces a `.nupkg` under
src/CodeBrix.Platform.Fonts.NotoMusic/bin/<Configuration>/.

There is no code generation and no native build step. The test csproj
links the `.ttf`, the `.ttf.manifest` and the `.uprimarker` into
TestAssets/ with CopyToOutputDirectory="PreserveNewest"; at about
176 KB the copy costs nothing.


TESTING
========================================================================

  dotnet test CodeBrix.Platform.Fonts.NotoMusic.slnx

No opt-in environment variables, no special preparation, no network
access. The suite is pure file/JSON/assembly inspection: xUnit v3 plus
SilverAssertions, with `TestContext.Current.CancellationToken` threaded
through any cancellable call.

What the three test classes pin:

  ContentManifestTests      The manifest deserializes and has a `fonts`
                            array; exactly one entry; weight 400 only;
                            Normal stretch only; upright only; the entry
                            points at the dash-free NotoMusic.ttf; every
                            `family_name` rooted at
                            `ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/`
                            and naming a file that exists; and no foreign
                            family token (RobotoMono, Iosevka, Sans,
                            Georgian, Merriweather) copied in from the
                            sibling package this one was mirrored from.
                            A stray "Noto" token cannot be tested for —
                            it legitimately appears in every path here.
  ContentFilePresenceTests  NotoMusic.ttf and its manifest are present;
                            exactly one `.ttf` ships; no dash-bearing
                            file survives; no upstream "Regular" name
                            token survives; `.uprimarker` present and
                            empty; the font is a non-trivial size.
  AssemblyMetadataTests     Assembly loads by name, simple name matches,
                            targets .NET 10, exports no public types.

The test project references the library by ProjectReference, so the
tests run against the freshly built assembly, not a restored package.


PACKAGING AND PUBLISHING
========================================================================

Pack driver: `GeneratePackageOnBuild=true` on the library csproj; there
is no separate pack script in this repository.

Versioning: date-stamped and auto-incrementing, computed in the csproj
from `System.DateTime.UtcNow` as 1.<years-since-2026>.<day-of-year>.
<minute-of-day>. Consequences worth remembering: every build yields a new
version; two builds inside the same UTC minute yield the SAME version, so
never publish two packages from within one minute; and the scheme is not
SemVer, so major/minor say nothing about API compatibility. Re-baseline
by changing `_VersionBaseYear`.

What the csproj packs:

  root of the nupkg   icon-codebrix-128.png, README.md, AGENT-README.txt,
                      THIRD-PARTY-NOTICES.txt, OFL.txt
  lib/net10.0         the assembly and
                      CodeBrix.Platform.Fonts.NotoMusic.uprimarker
  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/
                      `Fonts/*.ttf` and `Fonts/*.ttf.manifest`

There is no buildTransitive item group here — this package injects
nothing into a consumer build.

MAINTAINER-README.txt, EXTRAS-README.txt and README-INDEX.txt are
repository-only files: they are NOT packed. AGENT-README.txt is the file
that ships to consumers, so a consumer-facing correction belongs there.

Package metadata that must not drift: `PackageLicenseExpression` is
`OFL-1.1`; `PackageRequireLicenseAcceptance` is true; `PackageId` is
`CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever` while
`AssemblyName` / `RootNamespace` / `Product` / `Title` are
`CodeBrix.Platform.Fonts.NotoMusic`.

Ship this package as part of the CodeBrix.Platform family release, not on
its own.


PROVENANCE AND VENDORED SOURCES
========================================================================

Not a port of any upstream packaging project. The csproj, the
`.ttf.manifest`, the `.uprimarker` and all documentation are original
CodeBrix-family files, authored by mirroring the sibling
CodeBrix.Platform.Fonts.RobotoMono package (the most recent font package
at the time) — which is why the manifest tests screen for that package's
family tokens.

The only third-party material is the Noto Music `.ttf` binary,
redistributed bit-for-bit unmodified. Its provenance and the SIL OFL 1.1
terms are recorded in THIRD-PARTY-NOTICES.txt (binary `.ttf` files
cannot carry an inline provenance comment).

Font source and identity as bundled (read from the font's `name` table,
recorded here so a refresh can be compared against it):

  Family ....... Noto Music
  Subfamily .... Regular
  Version ...... 2.003; ttfautohint (v1.8.4.7-5d5b)
  Copyright .... Copyright 2022 The Noto Project Authors
  Upstream ..... https://github.com/notofonts/music
                 (downloaded from Google Fonts)
  Rename ....... NotoMusic-Regular.ttf -> NotoMusic.ttf (file name only;
                 the internal name tables still read "NotoMusic-Regular")

Noto Music declares no Reserved Font Name, so SIL OFL 1.1 condition 3
imposes no naming restriction. The bytes are still redistributed
unmodified; renames are fine, byte edits are not.

Refreshing the font — the checklist:

  1. Download from upstream; keep the bytes untouched.
  2. Rename to the dash-free `NotoMusic.ttf` (tests assert that no
     dash-bearing file and no "Regular" name token ships).
  3. Leave the manifest at its single Normal/400/Normal entry unless
     upstream actually publishes more faces.
  4. Update THIRD-PARTY-NOTICES.txt with the new version.
  5. Re-measure the `cmap` and update the GLYPH COVERAGE section and the
     quick reference card in AGENT-README.txt in the same change.


CODING CONVENTIONS
========================================================================

Standard CodeBrix family conventions apply; the ones that bite here:

  * Target framework: net10.0 only. No multi-targeting.
  * Nullable reference types: OFF (do not set `<Nullable>enable</Nullable>`).
    No `?` annotations on reference types, no `!` null-forgiveness.
    Value-type nullables are fine.
  * No global usings.
  * `<GenerateDocumentationFile>true</GenerateDocumentationFile>` is on;
    every public member of a public type needs an XML doc comment and
    CS1591 is fixed at source, never suppressed. The library currently
    has no public types, so this is trivially satisfied — it stops being
    trivial the moment anyone adds one.
  * No project-level warning suppression (`<NoWarn>`, `<WarningLevel>0`,
    `<TreatWarningsAsErrors>false</>` and friends are forbidden).
  * Tests: xUnit v3 + SilverAssertions, one `<Class>Tests.cs` per subject,
    snake_case test method names, //Arrange //Act //Assert comment
    blocks, `TestContext.Current.CancellationToken` on cancellable calls.
  * Every packaging library ships an InternalsVisibleTo.cs granting its
    `.Tests` assembly access.
  * The whole package — wrapper and font — is SIL OFL 1.1.

For the full list of family conventions see
CODEBRIX_LIBRARY_OBSERVATIONS.txt in the CodeBrix.Library.Dev-private
repository.


NOTES
========================================================================

  * DOCUMENTATION DEFECT TO FIX ELSEWHERE: README.md and
    THIRD-PARTY-NOTICES.txt both describe the font as carrying "a small
    set of supporting Latin, Greek and Cyrillic characters". The shipped
    font's `cmap` contains NO Greek (U+0370..U+03FF) and NO Cyrillic
    (U+0400..U+04FF) codepoints at all — the supporting set is Latin,
    punctuation and combining marks only, and the "Greek" in this font is
    the ancient musical-notation block U+1D200..U+1D245. AGENT-README.txt
    now states the measured coverage; the two files above still carry the
    old wording and were out of scope for this pass.
  * Coverage figures quoted in AGENT-README.txt were measured from the
    bundled font's `cmap`: 890 codepoints total, 556 of them musical
    (U+1D000..U+1D0F5 = 246, U+1D100..U+1D126 = 39, U+1D129..U+1D1EA =
    194, U+1D200..U+1D245 = 70, U+2669..U+266F = 7). Re-measure after any
    font refresh.
  * Why there is no `.targets` file: the prune target in the sibling
    packages removes dash-bearing static font files when
    `SupportsFontManifest` is not `'true'`. This package's only font is
    dash-free, so there would be nothing for such a target to do. If
    upstream ever publishes additional static instances, revisit the
    question then — upstream has published one face for years.
  * Why there is no CODEBRIX-DEVELOP.json: the descriptor exists so
    CodeBrix.Develop can offer a font as an application TEXT face in its
    New Application experience. Noto Music is a symbols companion, so it
    is deliberately not offered and carries no descriptor, no
    `resourceKey` and no `fallbackFontUris`. Adding one would put a
    symbols font in the IDE's text-font picker.
  * `bin/` and `obj/` folders in this working tree may contain stale
    `.nupkg` files from earlier builds; they are not authoritative.
