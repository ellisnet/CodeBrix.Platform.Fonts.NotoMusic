========================================================================
EXTRAS-README: CodeBrix.Platform.Fonts.NotoMusic
Samples, tools and other content in this repository that is not part of
a NuGet package
========================================================================

This repository contains NO sample applications, demo apps, tools,
scripts or optional test-data downloads. It is a single font asset
package plus the test project that guards it.

The only non-package content is the test project:

  tests/CodeBrix.Platform.Fonts.NotoMusic.Tests/
      xUnit v3 + SilverAssertions test project. It is not packed and is
      not published; it exists to pin the package's contents (the single
      font file, the one-entry manifest, the `.uprimarker` and the
      assembly metadata). Run it with:

          dotnet test CodeBrix.Platform.Fonts.NotoMusic.slnx

      No opt-in environment variables, no downloads, no special prep.
      See MAINTAINER-README.txt for what each test class asserts.

One thing that might look like repository content but is not: `bin/` and
`obj/` folders may hold `.nupkg` and build artefacts from earlier local
builds. They are build output, not repository content.
