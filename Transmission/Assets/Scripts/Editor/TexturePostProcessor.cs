using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TexturePostProcessor : AssetPostprocessor
{

    void OnPreprocessTexture()
    {
        TextureImporter importer = assetImporter as TextureImporter;

        importer.spritePixelsPerUnit = 16;
        importer.compressionQuality = 0;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.filterMode = FilterMode.Point;
    }
}