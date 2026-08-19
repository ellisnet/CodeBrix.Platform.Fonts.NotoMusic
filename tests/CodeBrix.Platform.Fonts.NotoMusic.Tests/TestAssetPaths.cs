using System;
using System.IO;

namespace CodeBrix.Platform.Fonts.NotoMusic.Tests;

internal static class TestAssetPaths
{
    public static string TestAssetsRoot { get; } =
        Path.Combine(AppContext.BaseDirectory, "TestAssets");

    public static string FontsFolder { get; } =
        Path.Combine(TestAssetsRoot, "Fonts");

    public static string ManifestPath { get; } =
        Path.Combine(FontsFolder, "NotoMusic.ttf.manifest");

    public static string FontPath { get; } =
        Path.Combine(FontsFolder, "NotoMusic.ttf");

    public static string UprimarkerPath { get; } =
        Path.Combine(TestAssetsRoot, "CodeBrix.Platform.Fonts.NotoMusic.uprimarker");
}
