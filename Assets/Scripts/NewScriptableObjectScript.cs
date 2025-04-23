using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GeoJsonData", menuName = "ScriptableObjects/GeoJsonData", order = 1)]
public class GeoJsonData : ScriptableObject
{
    public List<GeoFeature> features;
}

[System.Serializable]
public class GeoFeature
{
    public string type;
    public GeoGeometry geometry;
    // プロパティ情報が必要な場合はここに追加
}

[System.Serializable]
public class GeoGeometry
{
    public string type; // "Polygon"や"MultiPolygon"
    // GeoJSONの座標はネストされた配列になっているので、状況に合わせて構造体を定義します
    // ここでは例として、1つのポリゴンの場合の座標（外枠のみ）をVector2のリストとして扱います
    public List<List<Vector2>> coordinates;
}
