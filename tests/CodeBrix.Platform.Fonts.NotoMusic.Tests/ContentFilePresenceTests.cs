using System.IO;
using System.Linq;
using SilverAssertions;
using Xunit;

namespace CodeBrix.Platform.Fonts.NotoMusic.Tests;

public class ContentFilePresenceTests
{
    [Fact]
    public void Font_NotoMusic_ttf_is_present()
        => File.Exists(TestAssetPaths.FontPath).Should().BeTrue();

    [Fact]
    public void Manifest_file_is_present()
        => File.Exists(TestAssetPaths.ManifestPath).Should().BeTrue();

    [Fact]
    public void Total_ttf_count_is_1()
    {
        //Arrange/Act
        // Noto Music publishes exactly one face upstream (Regular, static —
        // no variable font, no italics, no other weights), so this package
        // ships exactly one .ttf.
        var ttfFiles = Directory.GetFiles(TestAssetPaths.FontsFolder, "*.ttf");

        //Assert
        ttfFiles.Length.Should().Be(1);
    }

    [Fact]
    public void No_dash_bearing_font_ships()
    {
        //Arrange — the upstream NotoMusic-Regular.ttf is renamed to the
        //dash-free NotoMusic.ttf on the way in. In the sibling font packages
        //the buildTransitive prune removes dash-bearing statics on platforms
        //without manifest support; keeping this package's only font dash-free
        //guarantees it survives that convention everywhere. This test keeps
        //the rename a decision rather than an accident.
        var offenders = Directory.GetFiles(TestAssetPaths.FontsFolder, "*.ttf")
            .Select(Path.GetFileName)
            .Where(name => name.Contains('-'))
            .ToList();

        //Assert
        offenders.Should().BeEmpty();
    }

    [Fact]
    public void No_upstream_Regular_name_token_survives()
    {
        //Arrange
        var offenders = Directory.GetFiles(TestAssetPaths.FontsFolder, "*.ttf")
            .Select(Path.GetFileName)
            .Where(name => name.Contains("Regular"))
            .ToList();

        //Assert
        offenders.Should().BeEmpty();
    }

    [Fact]
    public void Uprimarker_file_is_present()
        => File.Exists(TestAssetPaths.UprimarkerPath).Should().BeTrue();

    [Fact]
    public void Uprimarker_file_is_empty()
    {
        //Arrange
        var info = new FileInfo(TestAssetPaths.UprimarkerPath);

        //Assert
        info.Length.Should().Be(0L);
    }

    [Fact]
    public void Font_is_non_trivial_size()
    {
        //Arrange
        var info = new FileInfo(TestAssetPaths.FontPath);

        //Assert
        info.Length.Should().BeGreaterThan(100_000L);
    }
}
