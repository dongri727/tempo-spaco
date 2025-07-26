// Assets/Scripts/GeoConverter.cs
using UnityEngine;

/// <summary>
/// 緯度・経度（度）を Unity ワールド座標に変換する共通ユーティリティ。<br/>
/// X = 経度 (東＋), Y = 高さ, Z = 緯度 (北＋)
/// </summary>
public static class GeoConverter
{
    /// <summary>1 度あたり何 Unity Unit か（既存環境は 10u）</summary>
    public const float UnitsPerDegree = 10f;

    /// <summary>
    /// 緯度経度 → ワールド座標
    /// </summary>
    /// <param name="latitudeDeg">緯度（+ 北 / – 南）</param>
    /// <param name="longitudeDeg">経度（+ 東 / – 西）</param>
    /// <param name="y">高さ（任意）</param>
    public static Vector3 ToScene(float latitudeDeg, float longitudeDeg, float y = 0f)
    {
        return new Vector3(
            longitudeDeg * UnitsPerDegree, // X
            y,                              // Y
            latitudeDeg * UnitsPerDegree    // Z
        );
    }

    /// <summary>
    /// ワールド座標 → 緯度経度（度）
    /// </summary>
    public static (float latitudeDeg, float longitudeDeg) ToGeo(Vector3 scenePos)
    {
        return (
            scenePos.z / UnitsPerDegree, // 緯度
            scenePos.x / UnitsPerDegree  // 経度
        );
    }
}