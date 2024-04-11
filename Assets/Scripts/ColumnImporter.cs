using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ColumnImporter : MonoBehaviour
{
    public GameObject columnPrefab; // プレハブを指定
    public string fileName = "Columns"; // CSVファイル名

    void Start()
    {
        // CSVファイルを読み込み、行ごとに分割する
        TextAsset csvData = Resources.Load<TextAsset>(fileName);
        string[] lines = csvData.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if(parts.Length < 2) continue; // フィールドが足りない行は無視

            float lat = float.Parse(parts[0]) * 10;
            float lon = float.Parse(parts[1]) * 10;

            // オブジェクトを生成し、位置とテキストを設定
            GameObject columnObject = Instantiate(columnPrefab, new Vector3(lon, 0, lat), Quaternion.identity);
 
        }
    }
}
