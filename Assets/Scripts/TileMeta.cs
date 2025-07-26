// Assets/Scripts/TileMeta.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Chronomap/Tile Meta", fileName = "TileMeta")]
public class TileMeta : ScriptableObject
{
    [Header("Geographic Span (Degrees)")]
    [Tooltip("このタイルの中心経度（0, 45, 90 …）")]
    public float centerLongitude = 0f;

    [Tooltip("タイルがカバーする最小緯度（例: -23.43）")]
    public float latitudeMin = -23.43f;

    [Tooltip("タイルがカバーする最大緯度（例: 23.43）")]
    public float latitudeMax = 23.43f;

    [Header("Layer / Time Index")]
    [Tooltip("時間軸の層インデックス（0 = 第1層: 紀元1世紀）")]
    public int layerIndex = 0;
}