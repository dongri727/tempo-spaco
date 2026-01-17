using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVTrapezoid : MonoBehaviour
{
    public GameObject pointerPrefab; // プレハブを指定
    public string fileName = "Map"; // CSVファイル名

    void Start()
    {
        // CSVファイルを読み込み、行ごとに分割する
        TextAsset csvData = Resources.Load<TextAsset>(fileName);
        string[] lines = csvData.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if(parts.Length < 8) continue; // フィールドが足りない行は無視

            string date = parts[1];
            string name = parts[2];
            string country = parts[3];
            string city = parts[4];
            float x = float.Parse(parts[6]) * 10;
            float y = float.Parse(parts[7]);
            float z = float.Parse(parts[5]) * 10;

            // オブジェクトを生成し、位置とテキストを設定
            GameObject cityObject = Instantiate(pointerPrefab, new Vector3(x, y, z), Quaternion.identity);
            cityObject.name = name;
            PopUp popUpComponent = cityObject.GetComponent<PopUp>();
            if(popUpComponent != null)
            {
                popUpComponent.SetData($"{date}, {name},\n {country}, {city}");
            }
        }
    }
}
