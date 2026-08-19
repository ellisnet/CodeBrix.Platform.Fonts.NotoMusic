# CodeBrix.Platform.Fonts.NotoMusic

A redistribution of the Noto Music font packaged as a CodeBrix-family NuGet library for .NET 10 applications.
CodeBrix.Platform.Fonts.NotoMusic is a content-files font package for CodeBrix.Platform-forked applications — supplying the Noto Music font as a build-time asset — and is equally usable as a plain content-files NuGet in any .NET 10 project that wants the Noto Music font.
Noto Music is a musical-notation symbols font, not a text face: it covers the Unicode music blocks — Western musical symbols, Byzantine musical symbols and ancient Greek musical notation — plus the miscellaneous-symbols music characters and a small set of supporting Latin, Greek and Cyrillic characters. It is intended to be referenced alongside (not instead of) one of the family's text font packages, such as CodeBrix.Platform.Fonts.OpenSans or CodeBrix.Platform.Fonts.Roboto.
The library has no managed dependencies other than .NET, and is provided as a .NET 10 library and associated `CodeBrix.Platform.Fonts.NotoMusic.OflLicenseForever` NuGet package.

CodeBrix.Platform.Fonts.NotoMusic supports applications and assemblies that target Microsoft .NET version 10.0 and later.
Microsoft .NET version 10.0 is a Long-Term Supported (LTS) version of .NET, and was released on Nov 11, 2025; and will be actively supported by Microsoft until Nov 14, 2028.
Please update your C#/.NET code and projects to the latest LTS version of Microsoft .NET.

## CodeBrix.Platform.Fonts.NotoMusic supports:

* The single Noto Music static font (`NotoMusic.ttf`) — Noto Music publishes exactly one face upstream (Regular weight, upright, Normal stretch; no variable font and no italics), renamed here from `NotoMusic-Regular.ttf` to the dash-free family name used across the CodeBrix font packages.
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

## License

The entire package — the library code, the packaging wrapper, and the bundled Noto Music `.ttf` font file — is licensed under the SIL Open Font License, Version 1.1. see: https://en.wikipedia.org/wiki/SIL_Open_Font_License

The full license text is bundled with this repository at the repository root — as `LICENSE` and as the byte-identical `OFL.txt`, which is also packaged inside the produced NuGet. The package is published under the SPDX expression `OFL-1.1`.

Noto Music declares no Reserved Font Name, and the `.ttf` is redistributed bit-for-bit unmodified (the file is renamed on the way in; the bytes are untouched). See `THIRD-PARTY-NOTICES.txt` for the full attribution.
