========================================================================
AGENT-README: CodeBrix.Platform.Fonts.NotoMusic
A Guide for AI Coding Agents — CONSUMING the
CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever NuGet package
========================================================================


OVERVIEW
========================================================================

CodeBrix.Platform.Fonts.NotoMusic is a redistribution of the Noto Music
font, packaged as a content-asset NuGet library. It supplies the font as
a build-time content asset for CodeBrix.Platform applications, and is
equally usable as a plain content-files NuGet in any .NET 10 project
that wants the font binary.

Target framework: .NET 10 or later.

Noto Music is a MUSICAL-NOTATION SYMBOLS font, not a text face. It
carries clefs, noteheads, rests, accidentals, dynamics, articulations,
Byzantine neumes and ancient Greek vocal/instrumental notation — plus a
supporting set of Latin letters, digits and punctuation for notation
labels. Reference it ALONGSIDE one of the family's text font packages
(CodeBrix.Platform.Fonts.OpenSans, ...Roboto, ...RobotoMono,
...Merriweather), never instead of one.

This is the family's structurally simplest font package, because Noto
Music publishes exactly ONE face upstream: Regular weight, upright,
Normal stretch, static — no variable font, no italics, no other weights.
Where the sibling packages ship a variable font plus a set of static
instances per family, this package ships one `.ttf` (about 176 KB) and a
one-entry manifest.

The assembly contains no managed code that a consumer calls: it is a
metadata-only .NET 10 DLL whose only purpose is to carry the bundled
font content file. Everything a consumer uses is a file path, not a
type. What ships:

  - 1 `.ttf` font file, `NotoMusic.ttf`.
  - 1 `.ttf.manifest` JSON file with a single Normal / 400 / Normal
    entry pointing at that file.
  - A `.uprimarker` file that CodeBrix.Platform build pipelines use to
    discover font asset packages.

Two deliberate STRUCTURAL OMISSIONS relative to the sibling packages:

  1. NO buildTransitive `.targets` file. The siblings use one to prune
     their dash-bearing static instances at consumer-build time on
     platforms without manifest support. This package's only font is
     dash-free and must always be present, so there is nothing to prune.
     Nothing this package does can remove its font from your build.
  2. NO `CODEBRIX-DEVELOP.json` descriptor. Noto Music is not offered as
     an application text font in CodeBrix.Develop's "New
     CodeBrix.Platform Application" experience — it is a symbols
     companion, not a text face — so there is no descriptor to read, no
     `resourceKey` convention and no `fallbackFontUris` list for it.

Provenance: this package is not a port of any upstream packaging
project — the packaging files and documentation are original CodeBrix
work; the only third-party material is the `NotoMusic.ttf` binary
(upstream `NotoMusic-Regular.ttf`, Version 2.003), redistributed
bit-for-bit unmodified with the file RENAMED only, with full attribution
in the THIRD-PARTY-NOTICES.txt that ships inside the package.


INSTALLATION
========================================================================

NuGet package id: CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever

  dotnet add package CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever

NuGet dependencies: NONE. The package has no PackageReference of its
own; it carries one font binary and its manifest.

License: OFL-1.1 (SIL Open Font License 1.1). The whole package —
packaging wrapper and bundled font alike — is published under that one
SPDX expression, and the package sets
`PackageRequireLicenseAcceptance` to true, so restore in an interactive
or license-checking pipeline will require accepting it. `OFL.txt` is
packed at the root of the nupkg. Noto Music declares NO Reserved Font
Name, so SIL OFL 1.1 condition 3 places no naming restriction on this
redistribution.

The `.OflLicenseForever` suffix exists only on the NuGet package id, for
license disambiguation across the CodeBrix family. The assembly and the
`ms-appx:///` content root are both named
`CodeBrix.Platform.Fonts.NotoMusic`, with no suffix.

Requirements and limits:

  * No native libraries, no OS-specific components; the package is
    platform-neutral content.
  * `ms-appx:///` URIs are resolved by the CodeBrix.Platform runtime.
    Outside a CodeBrix.Platform host the URI means nothing; a plain
    .NET 10 app can still open the `.ttf`, but it has to locate the file
    itself under the package's `lib/net10.0/...` folder in the NuGet
    cache.
  * You almost always want a TEXT font package referenced as well —
    this package renders symbols, not prose.


KEY NAMESPACES / USINGS
========================================================================

There is nothing to `using`. The package exposes no public managed
types, so no namespace import is ever required to consume it.

The identifier that matters is the single content URI, whose root is the
ASSEMBLY name:

  ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf

That one URI is the entire addressable surface of the package.

Do NOT append a `#FamilyName` fragment to it. CodeBrix.Platform strips
the fragment before resolving the font, so it buys nothing — and on the
value assigned to `FeatureConfiguration.Font.DefaultTextFontFamily` it
actively breaks the startup font-manifest preload, because the
".manifest" suffix the preload appends lands inside the URI fragment and
is then dropped. (Setting Noto Music as the default TEXT font would be a
mistake anyway — it is a symbols font.)


FONT INVENTORY
========================================================================

The package ships 1 `.ttf` file plus 1 `.ttf.manifest` file.

  NotoMusic.ttf           The single upstream face: Regular, upright,
                          Normal stretch, static. About 176 KB. Renamed,
                          byte-for-byte, from the upstream file
                          `NotoMusic-Regular.ttf` (see OVERVIEW for
                          the bundled upstream version).

  NotoMusic.ttf.manifest  A JSON object with a `fonts` array holding
                          exactly one entry:

    {
      "font_style":   "Normal",
      "font_weight":  400,
      "font_stretch": "Normal",
      "family_name":  "ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf"
    }

  `family_name` holds the URI of the font file, not a typographic family
  name — do not be misled by the member name.

THE NO-VARIABLE-FONT QUIRK
------------------------------------------------------------------------

Noto Music publishes no variable font, so the dash-free `NotoMusic.ttf`
(the slot the sibling packages fill with a variable font) is the static
Regular instance itself, and the manifest's weight-400 entry points at
it directly. Consequences for a consumer:

  1. There is exactly one face. `FontWeight`, `FontStyle` and
     `FontStretch` have nothing else to resolve to; a request for bold
     or italic gets whatever synthesis the platform applies, which on a
     notation font usually looks wrong. Leave those properties alone.
  2. There is no `NotoMusic-Regular.ttf` in the package. The dash-free
     name is the only name; a URI containing a dash will not resolve.


GLYPH COVERAGE
========================================================================

Measured from the bundled font's `cmap`: 890 codepoints, of which 556
are musical.

MUSICAL BLOCKS (556 codepoints)

  U+1D000..U+1D0F5   246  Byzantine Musical Symbols (the whole
                          assigned range of the block)
  U+1D100..U+1D126   39   Musical Symbols — clefs and staff signs
  U+1D129..U+1D1EA   194  Musical Symbols — the rest of the assigned
                          range, up to and including the Kievan and
                          Persian additions (U+1D127 and U+1D128 are
                          unassigned in Unicode, hence the gap)
  U+1D200..U+1D245   70   Ancient Greek Musical Notation (the whole
                          assigned range of the block)
  U+2669..U+266F     7    Miscellaneous Symbols — the seven music
                          characters

Landmarks verified present in the font:

  U+1D11E  MUSICAL SYMBOL G CLEF        U+1D15D  WHOLE NOTE
  U+1D121  MUSICAL SYMBOL C CLEF        U+1D15E  HALF NOTE
  U+1D122  MUSICAL SYMBOL F CLEF        U+1D15F  QUARTER NOTE
  U+1D12A  DOUBLE SHARP                 U+1D160  EIGHTH NOTE
  U+1D12B  DOUBLE FLAT                  U+1D161  SIXTEENTH NOTE
  U+1D13B  WHOLE REST                   U+1D162  THIRTY-SECOND NOTE
  U+1D13C  HALF REST                    U+1D158  NOTEHEAD BLACK
  U+1D13D  QUARTER REST                 U+1D16D  COMBINING AUGMENTATION
  U+1D18F  DYNAMIC PIANO                         DOT
  U+1D183  ARPEGGIATO UP                U+1D1AA  COMBINING DOWN BOW
  U+1D1DE  KIEVAN C CLEF                U+1D1EA  KORON

  U+2669 QUARTER NOTE, U+266A EIGHTH NOTE, U+266B BEAMED EIGHTH NOTES,
  U+266C BEAMED SIXTEENTH NOTES, U+266D MUSIC FLAT SIGN,
  U+266E MUSIC NATURAL SIGN, U+266F MUSIC SHARP SIGN.

  The U+266x seven are the ones that fit in a single UTF-16 char and
  need no surrogate handling — prefer them for simple inline marks such
  as a sharp or flat next to a note name.

SUPPORTING TEXT CHARACTERS (334 codepoints)

  Basic Latin U+0020..U+007E in full; most of Latin-1 Supplement; most
  of Latin Extended-A; a handful of Latin Extended-B (U+0218..U+021B,
  U+0237); modifier letters U+02C6..U+02DD; combining diacritics
  U+0300..U+0328; U+1E80..U+1E85, U+1E9E, U+1EF2..U+1EF3; the common
  General Punctuation marks (U+2013, U+2014, quotes, bullet, ellipsis,
  guillemets); U+20AC EURO SIGN; U+2122 TRADE MARK SIGN; U+2212 MINUS
  SIGN; U+25CC DOTTED CIRCLE (the placeholder ring for combining marks).

  There is NO Greek and NO Cyrillic text coverage in the shipped font —
  the Greek here is the ancient-notation BLOCK (U+1D200..U+1D245), not
  the Greek alphabet. Do not use this font for Greek or Cyrillic prose.

  The supporting Latin exists for notation labels ("Allegro", "8va",
  rehearsal letters), not for body text. Use a text font for prose.

The CodeBrix family never falls back to a system font, so a codepoint
outside the coverage above renders as `.notdef` rather than borrowing a
glyph from the OS — and this font draws a BOX for `.notdef`, so a miss
shows up as a tofu box rather than vanishing.


WRITING MUSIC CHARACTERS: XAML AND C# ESCAPES
========================================================================

Every codepoint in the U+1D0xx..U+1D2xx blocks is ASTRAL (above U+FFFF).
That has two practical consequences: how you write the character, and
how .NET counts it.

IN XAML — use an XML numeric character reference, hexadecimal form:

    Text="&#x1D11E;"                       <!-- G clef            -->
    Text="&#x1D15F;&#x1D16D;"              <!-- dotted quarter    -->
    Text="&#x266F;"                        <!-- sharp sign (BMP)  -->

  * The `&#x....;` form takes the full Unicode scalar value — do NOT
    write a surrogate pair as two references.
  * XAML has no `\u` escape; a backslash escape in a XAML attribute is a
    literal backslash.
  * Decimal references (`&#119070;`) work too but are unreadable; prefer
    hex.
  * Pasting the literal character into a UTF-8 XAML file also works.

IN C# — use the 8-digit `\U` escape (capital U), or build it from the
scalar value:

    string gClef   = "\U0001D11E";                   // G clef
    string sharp   = "\u266F";                       // BMP, 4-digit \u
    string quarter = char.ConvertFromUtf32(0x1D15F);  // computed at run time

  * `\U` takes exactly EIGHT hex digits; `\u` takes exactly four and
    cannot express an astral codepoint on its own.
  * The surrogate-pair form `"\uD834\uDD1E"` is equivalent, but the
    `\U` form is clearer.

WHAT .NET COUNTS

  "\U0001D11E".Length is 2 — an astral character is one Unicode scalar
  stored as two UTF-16 chars. So:

  * Never split, truncate or index such a string by `char` position
    (`Substring`, `s[i]`, a manual reverse) — you will cut a surrogate
    pair in half and produce an unrenderable lone surrogate.
  * Enumerate with `System.Globalization.StringInfo`,
    `text.AsSpan().EnumerateRunes()` or `char.ConvertToUtf32`, and build
    with `char.ConvertFromUtf32(int)`.
  * A TextBox `MaxLength` of 1 cannot hold one of these characters.


USING IT NEXT TO A TEXT FACE
========================================================================

This package supplies no text face and no fallback wiring of its own:
it carries no CODEBRIX-DEVELOP.json, so it declares no fallback list,
and the sibling CodeBrix.Platform.Fonts.Merriweather descriptor lists
only its own three Noto Serif companions. Put the music glyphs on
screen by switching `FontFamily` on the RUN that carries them:

    <TextBlock FontFamily="ms-appx:///CodeBrix.Platform.Fonts.Merriweather/Fonts/Merriweather.ttf">
      <Run Text="Play the passage " />
      <Run Text="&#x1D11E;"
           FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf" />
      <Run Text=" as written." />
    </TextBlock>

`<Run>` inherits everything from the parent `TextBlock` except what it
overrides, so the surrounding prose keeps the text font and only the
symbol run uses Noto Music. The same applies to any element that takes a
`FontFamily`: `TextBlock`, `Run`, `TextBox`, `Button` content, and so
on.

Two things a per-run switch cannot do for you:

  * Optical matching. Noto Music's symbols are not designed to match a
    particular text face's weight or x-height; expect to tune `FontSize`
    (and sometimes a small vertical offset) rather than assuming the
    glyph will sit correctly at the text's size.
  * Automatic selection. Nothing makes a text font "reach" this font for
    a codepoint it lacks unless the platform's fallback chain is
    configured to do so, and this package does not configure it. Assume
    you must name the font on the run.


CORE API REFERENCE
========================================================================

This package has no public managed API — the assembly deliberately
exports zero public types (a test pins that). The complete consumer
surface is two things:

  1. THE FONT URI — used as a `FontFamily` value in XAML or in code that
     builds XAML element trees:

       ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf

     with no `#` fragment.

  2. THE MANIFEST beside it, `NotoMusic.ttf.manifest`, whose single
     Normal / 400 / Normal entry is what a manifest-driven platform
     resolves to. There is nothing to choose: any weight/style/stretch
     request that resolves through this package lands on the one file.

There is no MSBuild target, no MSBuild property and no descriptor in
this package — unlike its siblings, it injects nothing into your build.

If a future iteration of this package exposes a managed API (for
example typed accessors returning font streams or paths for
non-CodeBrix.Platform consumers), it will live under the
`CodeBrix.Platform.Fonts.NotoMusic` root namespace and be documented
here.


WHAT IS IN THE NUGET PACKAGE
========================================================================

Consumer-visible layout of the produced nupkg:

  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic.dll
  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic.uprimarker
  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf
  lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf.manifest
  AGENT-README.txt
  README.md
  OFL.txt
  THIRD-PARTY-NOTICES.txt
  icon-codebrix-128.png

The `lib/net10.0/CodeBrix.Platform.Fonts.NotoMusic/Fonts/` folder name is
load-bearing: the `ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/...`
URI resolves relative to the assembly name, so the folder and the
assembly always carry the same name.


COMPLETE EXAMPLES
========================================================================

1. A single music glyph
------------------------------------------------------------------------

    <TextBlock Text="&#x1D11E;"
               FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf"
               FontSize="48" />

2. A short notation legend
------------------------------------------------------------------------

    <StackPanel Orientation="Horizontal" Spacing="12">
      <TextBlock FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf"
                 FontSize="32"
                 Text="&#x1D11E; &#x1D121; &#x1D122;" />
      <TextBlock FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf"
                 FontSize="32"
                 Text="&#x1D15D; &#x1D15E; &#x1D15F; &#x1D160;" />
      <TextBlock FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf"
                 FontSize="32"
                 Text="&#x266D; &#x266E; &#x266F;" />
    </StackPanel>

3. Symbols inside prose (per-run FontFamily switch)
------------------------------------------------------------------------

    <TextBlock FontFamily="ms-appx:///CodeBrix.Platform.Fonts.Merriweather/Fonts/Merriweather.ttf"
               FontSize="16">
      <Run Text="The key signature adds one sharp (" />
      <Run Text="&#x266F;"
           FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf" />
      <Run Text=") and the theme opens on a dotted quarter " />
      <Run Text="&#x1D15F;&#x1D16D;"
           FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf" />
      <Run Text="." />
    </TextBlock>

4. Building the same text in C#
------------------------------------------------------------------------

    const string MusicFont =
        "ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf";

    // "\U0001D15F" is the quarter note; "\U0001D16D" the augmentation dot.
    string dottedQuarter = "\U0001D15F\U0001D16D";

    var block = new TextBlock();
    block.Inlines.Add(new Run { Text = "Opens on a dotted quarter " });
    block.Inlines.Add(new Run
    {
        Text = dottedQuarter,
        FontFamily = new FontFamily(MusicFont),
    });

  `TextBlock`, `Run` and `FontFamily` are CodeBrix.Platform types, not
  types from this package — take their namespaces and their exact
  constructors from that package's own documentation. What THIS package
  guarantees is the URI string and the codepoints.

5. Reading the font bytes without a CodeBrix.Platform host
------------------------------------------------------------------------

    // ms-appx:/// does not resolve in a console app or a unit test.
    // Locate the restored package content on disk instead:
    //   <nuget-cache>/codebrix.platform.fonts.notomusic.ofllicenseforever/
    //       <version>/lib/net10.0/
    //       CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf
    byte[] fontBytes = System.IO.File.ReadAllBytes(pathToNotoMusicTtf);


MINIMUM VIABLE PROJECT
========================================================================

MyApp.csproj — the music font plus a text font (a real CodeBrix.Platform
head project also references the platform packages):

    <Project Sdk="Microsoft.NET.Sdk">

      <PropertyGroup>
        <TargetFramework>net10.0</TargetFramework>
      </PropertyGroup>

      <ItemGroup>
        <PackageReference Include="CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever" />
        <!-- a text face for the prose around the symbols -->
        <PackageReference Include="CodeBrix.Platform.Fonts.Merriweather.OflLicenseForever" />
      </ItemGroup>

    </Project>

  This package needs no `SupportsFontManifest` setting and injects no
  MSBuild target; its font is always in the payload.

MainPage.xaml:

    <Page x:Class="MyApp.MainPage"
          xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
          xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
      <StackPanel Padding="24" Spacing="8">

        <TextBlock Text="&#x1D11E;"
                   FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf"
                   FontSize="64" />

        <TextBlock FontFamily="ms-appx:///CodeBrix.Platform.Fonts.Merriweather/Fonts/Merriweather.ttf"
                   FontSize="16">
          <Run Text="Treble clef, then a natural sign " />
          <Run Text="&#x266E;"
               FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf" />
          <Run Text=" inline." />
        </TextBlock>

      </StackPanel>
    </Page>

If the glyphs come out blank, the font is not in the payload or the URI
is wrong — check the spelling of the URI before suspecting the font.


PERFORMANCE TIPS
========================================================================

  * This is a static asset carrier: there is no managed code on any hot
    path, and no MSBuild work either. The only cost is one ~176 KB font
    file in the app payload and one font load at first use — the
    smallest footprint of any font package in the family.

  * Prefer the BMP characters U+2669..U+266F over their astral
    equivalents when either will do: no surrogate pairs to mishandle,
    and shorter strings.

  * Group symbol runs. One `<Run>` carrying several glyphs costs less
    layout work than one `<Run>` per glyph, and avoids repeated
    `FontFamily` switches inside a paragraph.


COMMON PITFALLS TO AVOID
========================================================================

  * Do not set Noto Music as an application's default text font, and do
    not assign its URI to `DefaultTextFontFamily`. Its Latin coverage
    exists for notation labels; prose set in it will be missing
    characters, and everything else in the app inherits the symbols
    font.

  * NEVER add a `#FamilyName` fragment to the font URI. CodeBrix.Platform
    strips it during font resolution, and on `DefaultTextFontFamily` it
    silently disables the startup manifest preload (the appended
    ".manifest" lands inside the fragment and is dropped by
    `Uri.PathAndQuery`).

  * Do not write `NotoMusic-Regular.ttf` in a URI. That upstream name
    does not exist in this package — the file is `NotoMusic.ttf`.

  * Do not request bold or italic. There is one face; any "bold" you get
    is synthetic and will not look like notation type.

  * Do not index or truncate strings containing astral music characters
    by `char` position — you will split a surrogate pair. See WRITING
    MUSIC CHARACTERS.

  * Do not expect Greek or Cyrillic prose glyphs from this font: the
    shipped `cmap` has none. The "Greek" in this font is the ancient
    musical-notation block.

  * There is no system-font fallback anywhere in the CodeBrix family.
    A codepoint this font lacks will not be borrowed from the OS; it
    renders as `.notdef`, which this font draws as a box.

  * `ms-appx:///` is a CodeBrix.Platform concept, not a .NET one. In a
    console app or unit test the URI will not resolve; locate the file on
    disk instead.

  * Do not expect this package to add a `.targets` file or a
    CODEBRIX-DEVELOP.json descriptor to your build. It has neither by
    design, so tooling that looks for a font descriptor will not find
    one here.


WHAT THIS PACKAGE DOES NOT DO
========================================================================

  * It exposes no public managed types, no font-loading helper and no
    glyph-metrics API. Referencing it gives you a file, not objects.
  * It does not render or lay out music. It is a set of glyphs, not a
    notation engine: no staves are drawn for you, no beaming, no
    spacing, no MusicXML or MIDI handling.
  * It is not a text face, and it must not be an application's default
    font.
  * It ships no variable font, no italics and no weights other than
    Regular — upstream publishes exactly one face.
  * It ships no buildTransitive `.targets` file and no
    CODEBRIX-DEVELOP.json descriptor, so it contributes no MSBuild
    target, no MSBuild property and no CodeBrix.Develop font choice.
  * It does not register itself as a fallback font for any text face,
    and it is not listed as one by the sibling
    CodeBrix.Platform.Fonts.Merriweather descriptor. Assume you must
    name this font explicitly on the run that needs it.
  * It does not install fonts into the operating system.
  * It does not fall back to a system font, and cannot make one
    available.
  * It has no runtime dependency on CodeBrix.Platform: nothing stops a
    non-platform project from referencing the package for the font file
    alone.


WORKING EXAMPLES ON GITHUB
========================================================================

The package's own test suite is the executable specification of every
claim above about what ships:

  https://github.com/ellisnet/CodeBrix.Platform.Fonts.NotoMusic/tree/main/tests/CodeBrix.Platform.Fonts.NotoMusic.Tests

  ContentManifestTests.cs     — the manifest deserializes, holds exactly
                                one entry, covers only weight 400,
                                Normal stretch and upright, points at the
                                dash-free `NotoMusic.ttf`, is rooted at
                                `ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/`
                                and names a file that exists.
  ContentFilePresenceTests.cs — exactly one `.ttf` ships, no dash-bearing
                                file survives, no upstream "Regular" name
                                token survives, and the `.uprimarker` is
                                present and empty.
  AssemblyMetadataTests.cs    — the assembly is named
                                `CodeBrix.Platform.Fonts.NotoMusic`,
                                targets .NET 10 and exports no public
                                types.

Repository root (README.md has a short XAML snippet too):

  https://github.com/ellisnet/CodeBrix.Platform.Fonts.NotoMusic


QUICK REFERENCE CARD
========================================================================

Package id .... CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever
License ....... OFL-1.1 (acceptance required; no Reserved Font Name)
Dependencies .. none          Target ........ .NET 10 or later
Public types .. none          Descriptor .... none (no CODEBRIX-DEVELOP.json)
MSBuild ....... none (no .targets file, no properties)

THE ONE URI

  ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf

  * no `#FamilyName` fragment, ever
  * one face only: Normal style, weight 400, Normal stretch

COVERAGE (890 codepoints; 556 musical)

  U+1D000..U+1D0F5   Byzantine Musical Symbols          246
  U+1D100..U+1D126   Musical Symbols (clefs, staves)     39
  U+1D129..U+1D1EA   Musical Symbols (notes .. Persian) 194
  U+1D200..U+1D245   Ancient Greek Musical Notation      70
  U+2669..U+266F     Miscellaneous Symbols (music)        7
  Latin/punctuation for labels                           334
  no Greek or Cyrillic prose glyphs; no other scripts

HANDY CODEPOINTS

  G clef .... U+1D11E    whole note ..... U+1D15D
  C clef .... U+1D121    half note ...... U+1D15E
  F clef .... U+1D122    quarter note ... U+1D15F
  flat ...... U+266D     eighth note .... U+1D160
  natural ... U+266E     16th note ...... U+1D161
  sharp ..... U+266F     aug. dot ....... U+1D16D
  whole rest  U+1D13B    half rest ...... U+1D13C
  qtr rest .. U+1D13D    notehead black . U+1D158

ESCAPES

  XAML .......... Text="&#x1D11E;"      (hex XML character reference)
  C# (astral) ... "\U0001D11E"          (exactly 8 hex digits)
  C# (BMP) ...... "\u266F"             (exactly 4 hex digits)
  C# (computed) . char.ConvertFromUtf32(0x1D11E)
  Length ........ "\U0001D11E".Length == 2 — never split by char index

RULES

  * Symbols font — pair it with a text font; never make it the default.
  * Switch FontFamily per <Run>; nothing selects this font for you.
  * No system-font fallback; uncovered codepoints render as a .notdef
    box.
