using UnityEditor;
using System.IO;
using UnityEngine;

public class BuildAssetBundles
{
    // Nama folder output
    static string outputDirectory = "AssetBundles";

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        // Tentukan platform target saat ini
        BuildTarget targetPlatform = EditorUserBuildSettings.activeBuildTarget;

        // Buat path folder output spesifik platform
        // Contoh: "Assets/../AssetBundles/Android"
        string platformOutputDirectory = Path.Combine(outputDirectory, targetPlatform.ToString());

        // Pastikan folder output ada
        if (!Directory.Exists(platformOutputDirectory))
        {
            Directory.CreateDirectory(platformOutputDirectory);
            Debug.Log($"Folder output dibuat: {platformOutputDirectory}");
        }

        Debug.Log($"Mulai build AssetBundles untuk {targetPlatform} ke {platformOutputDirectory}...");

        // Build AssetBundles
        AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(
            platformOutputDirectory,
            BuildAssetBundleOptions.ChunkBasedCompression,
            targetPlatform
        );

        if (manifest != null)
        {
            Debug.Log($"Build AssetBundles Selesai untuk {targetPlatform}.");
            // Menampilkan folder output di explorer
            EditorUtility.RevealInFinder(platformOutputDirectory);
        }
        else
        {
            Debug.LogError("Build AssetBundles Gagal!");
        }
    }
}