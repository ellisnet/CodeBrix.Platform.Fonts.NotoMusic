# CodeBrix.Platform.Fonts.NotoMusic

A redistribution of the Noto Music font packaged as a CodeBrix-family NuGet library for .NET 10 applications.
CodeBrix.Platform.Fonts.NotoMusic is a content-files font package for CodeBrix.Platform applications — supplying the Noto Music font as a build-time asset — and is equally usable as a plain content-files NuGet in any .NET 10 project that wants the Noto Music font.
Noto Music is a musical-notation symbols font, not a text face: it covers the Unicode music blocks — Western musical symbols, Byzantine musical symbols and ancient Greek musical notation — plus the miscellaneous-symbols music characters and a small set of supporting Latin letters, digits and punctuation for notation labels (it has no Greek or Cyrillic text glyphs). It is intended to be referenced alongside (not instead of) one of the family's text font packages, such as CodeBrix.Platform.Fonts.OpenSans or CodeBrix.Platform.Fonts.Roboto.
The library has no managed dependencies other than .NET, and is provided as a .NET 10 library and associated `CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever` NuGet package.

CodeBrix.Platform.Fonts.NotoMusic supports applications and assemblies that target Microsoft .NET version 10.0 and later.
Microsoft .NET version 10.0 is a Long-Term Supported (LTS) version of .NET, and was released on Nov 11, 2025; and will be actively supported by Microsoft until Nov 14, 2028.
Please update your C#/.NET code and projects to the latest LTS version of Microsoft .NET.

## Installation

```
dotnet add package CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever
```

Note that the NuGet package ID and the assembly name are different - there is no package named plain `CodeBrix.Platform.Fonts.NotoMusic`:

* NuGet package ID: `CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever`
* Assembly and content-folder name: `CodeBrix.Platform.Fonts.NotoMusic` - the name that the `ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/...` URI shown below resolves against.

The assembly carries no managed API and nothing to `using` - everything a consumer uses is a font file path. The package has no dependencies beyond .NET itself; add one of the family's text font packages alongside it for the letters, digits and punctuation around your notation glyphs.

## CodeBrix.Platform.Fonts.NotoMusic supports:

* The single Noto Music static font (`NotoMusic.ttf`) — Noto Music is published as exactly one face (Regular weight, upright, Normal stretch; no variable font and no italics), and it carries the dash-free family name used across the CodeBrix font packages.
* A `NotoMusic.ttf.manifest` JSON file whose single entry maps the Normal/400/Normal `font_style` / `font_weight` / `font_stretch` triple to the font file — for platforms that resolve fonts through the static-instance manifest.
* The CodeBrix `.uprimarker` file so CodeBrix.Platform build pipelines discover the package as a UPRI-bearing font asset library.

Unlike the sibling font packages, this package deliberately ships **no** `buildTransitive` MSBuild `.targets` file (its only font is dash-free and must always be present, so there are no redundant static instances to prune) and **no** `CODEBRIX-DEVELOP.json` descriptor (Noto Music is not offered as an application text font in CodeBrix.Develop's New Application experience).

## Sample Code

### Reference the font from XAML (CodeBrix.Platform app)

```xml
<TextBlock Text="&#x1D11E; &#x1D122; &#x266A;"
           FontFamily="ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/NotoMusic.ttf" />
```

Note that the font URI carries no `#FamilyName` fragment. CodeBrix.Platform strips such a fragment before resolving the font, and leaving it on prevents the startup font-manifest preload from finding the manifest.

## Documentation

The NuGet package includes `AGENT-README.txt`, a complete reference and usage guide written for AI coding agents - point your agent at that file when it is writing code or XAML against this package. It covers the shipped font, the manifest format and the glyph coverage a consumer can rely on.

Additional sample code and usage examples are available in the `CodeBrix.Platform.Fonts.NotoMusic.Tests` project:
https://github.com/ellisnet/CodeBrix.Platform.Fonts.NotoMusic/tree/main/tests/CodeBrix.Platform.Fonts.NotoMusic.Tests

## License

CodeBrix.Platform.Fonts.NotoMusic is licensed under the SIL Open Font License, Version 1.1 - see the
[LICENSE](https://github.com/ellisnet/CodeBrix.Platform.Fonts.NotoMusic/blob/main/LICENSE) file. The licence
covers the entire package: the library code, the packaging wrapper, and the bundled Noto Music `.ttf` font file
alike. The same text is bundled at the repository root as the byte-identical `OFL.txt`, which is also packaged
inside the produced NuGet, and the package is published under the SPDX expression `OFL-1.1`.

Noto Music declares no Reserved Font Name, and the `.ttf` is redistributed bit-for-bit unmodified (the file is
renamed on the way in; the bytes are untouched).

For licensing and provenance information about the open source code included in
this package, see [THIRD-PARTY-NOTICES.txt](https://github.com/ellisnet/CodeBrix.Platform.Fonts.NotoMusic/blob/main/THIRD-PARTY-NOTICES.txt).
