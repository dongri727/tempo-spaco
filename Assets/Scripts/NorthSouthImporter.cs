using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class NorthSouthImporter : MonoBehaviour
{
    public GameObject northSouthPrefab; // プレハブを指定
    public string fileName = "NorthSouth"; // CSVファイル名

    void Start()
    {
        // CSVファイルを読み込み、行ごとに分割する
        TextAsset csvData = Resources.Load<TextAsset>(fileName);
        string[] lines = csvData.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if(parts.Length < 2) continue; // フィールドが足りない行は無視

            float hor = float.Parse(parts[0]) * 10;
            float ver = float.Parse(parts[1]);

            // オブジェクトを生成し、位置とテキストを設定
            GameObject columnObject = Instantiate(northSouthPrefab, new Vector3(hor, ver, 0), Quaternion.Euler(90, 0, 0));
 
        }
    }
}
