using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using SilverAssertions;
using Xunit;

namespace CodeBrix.Platform.Fonts.NotoMusic.Tests;

public class ContentManifestTests
{
    private const string CodeBrixPathPrefix = "ms-appx:///CodeBrix.Platform.Fonts.NotoMusic/Fonts/";

    // This package was authored by mirroring the sibling RobotoMono package
    // (the most recent font package at the time), so the realistic copy-paste
    // regressions are stray tokens from that package's four families. A stray
    // "Noto" token cannot be tested for — it legitimately appears in every
    // path here.
    private static readonly string[] ForeignFamilyTokens =
        ["RobotoMono", "Iosevka", "Sans", "Georgian", "Merriweather"];

    [Fact]
    public void Manifest_file_exists_in_test_output()
        => File.Exists(TestAssetPaths.ManifestPath).Should().BeTrue();

    [Fact]
    public void Manifest_can_be_deserialized()
    {
        //Arrange
        var json = File.ReadAllText(TestAssetPaths.ManifestPath);

        //Act
        var doc = JsonDocument.Parse(json);

        //Assert
        doc.RootElement.TryGetProperty("fonts", out var fonts).Should().BeTrue();
        fonts.ValueKind.Should().Be(JsonValueKind.Array);
    }

    [Fact]
    public void Manifest_has_exactly_one_entry()
    {
        //Arrange — Noto Music publishes a single face, so the manifest carries
        //a single entry.
        var entries = ReadManifestEntries(TestAssetPaths.ManifestPath);

        //Act/Assert
        entries.Count.Should().Be(1);
    }

    [Fact]
    public void Manifest_every_family_name_uses_codebrix_namespace()
    {
        //Arrange
        var entries = ReadManifestEntries(TestAssetPaths.ManifestPath);

        //Act
        var nonMatching = entries
            .Where(e => !e.FamilyName.StartsWith(CodeBrixPathPrefix))
            .ToList();

        //Assert
        nonMatching.Should().BeEmpty();
    }

    [Fact]
    public void Manifest_every_referenced_font_file_exists_on_disk()
    {
        //Arrange
        var entries = ReadManifestEntries(TestAssetPaths.ManifestPath);

        //Act
        var missing = entries
            .Select(e => Path.GetFileName(e.FamilyName))
            .Select(name => Path.Combine(TestAssetPaths.FontsFolder, name))
            .Where(path => !File.Exists(path))
            .ToList();

        //Assert
        missing.Should().BeEmpty();
    }

    [Fact]
    public void Manifest_covers_only_weight_400()
    {
        //Arrange — the single upstream face is Regular; there are no other
        //weights to map, and no fake entries are invented for weights the
        //font does not carry.
        var entries = ReadManifestEntries(TestAssetPaths.ManifestPath);

        //Act
        var distinctWeights = entries.Select(e => e.FontWeight).Distinct().ToArray();

        //Assert
        distinctWeights.Should().BeEquivalentTo(new[] { 400 });
    }

    [Fact]
    public void Manifest_is_normal_stretch_only()
    {
        //Arrange
        var entries = ReadManifestEntries(TestAssetPaths.ManifestPath);

        //Act
        var distinctStretches = entries.Select(e => e.FontStretch).Distinct().ToArray();

        //Assert
        distinctStretches.Should().BeEquivalentTo(new[] { "Normal" });
    }

    [Fact]
    public void Manifest_is_upright_only()
    {
        //Arrange — Noto Music publishes no italic face.
        var entries = ReadManifestEntries(TestAssetPaths.ManifestPath);

        //Act
        var distinctStyles = entries.Select(e => e.FontStyle).Distinct().ToArray();

        //Assert
        distinctStyles.Should().BeEquivalentTo(new[] { "Normal" });
    }

    [Fact]
    public void Manifest_regular_entry_points_at_the_dash_free_font()
    {
        //Arrange — like Iosevka in the sibling RobotoMono package, Noto Music
        //has no variable font, so the dash-free NotoMusic.ttf IS the Regular
        //static instance and the weight-400 entry references it directly.
        var entries = ReadManifestEntries(TestAssetPaths.ManifestPath);

        //Act
        var regular = entries.Single(e => e.FontWeight == 400);

        //Assert
        Path.GetFileName(regular.FamilyName).Should().Be("NotoMusic.ttf");
    }

    [Fact]
    public void Manifest_contains_no_foreign_family_tokens()
    {
        //Arrange
        var json = File.ReadAllText(TestAssetPaths.ManifestPath);

        //Act
        var offenders = ForeignFamilyTokens.Where(json.Contains).ToList();

        //Assert
        offenders.Should().BeEmpty();
    }

    private static List<ManifestEntry> ReadManifestEntries(string manifestPath)
    {
        var json = File.ReadAllText(manifestPath);
        using var doc = JsonDocument.Parse(json);
        var fonts = doc.RootElement.GetProperty("fonts");

        var list = new List<ManifestEntry>(fonts.GetArrayLength());
        foreach (var entry in fonts.EnumerateArray())
        {
            list.Add(new ManifestEntry(
                entry.GetProperty("font_style").GetString() ?? string.Empty,
                entry.GetProperty("font_weight").GetInt32(),
                entry.GetProperty("font_stretch").GetString() ?? string.Empty,
                entry.GetProperty("family_name").GetString() ?? string.Empty));
        }
        return list;
    }

    private readonly record struct ManifestEntry(
        string FontStyle,
        int FontWeight,
        string FontStretch,
        string FamilyName);
}
