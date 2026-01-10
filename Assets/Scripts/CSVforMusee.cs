using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVforMusee : MonoBehaviour
{
    public GameObject pointerPrefab; // プレハブを指定
    public string fileName = "Map"; // CSVファイル名

    [Header("Spawn settings")]
    [Tooltip("Optional parent for spawned pointers. Useful for moving/scaling all pointers together.")]
    public Transform spawnParent;

    [Tooltip("Offset applied after scaling, in local space of spawnParent (or world space if no parent).")]
    public Vector3 offset = Vector3.zero;

    [Header("Axis scaling")]
    [Tooltip("Scale applied to X (longitude-like) values from CSV.")]
    public float xScale = 1f;

    [Tooltip("Scale applied to Y (height/time-like) values from CSV.")]
    public float yScale = 1f;

    [Tooltip("Scale applied to Z (latitude-like) values from CSV.")]
    public float zScale = 1f;

    [Header("CSV columns")]
    [Tooltip("0-based column index for X value in CSV.")]
    public int xCol = 6;

    [Tooltip("0-based column index for Y value in CSV.")]
    public int yCol = 7;

    [Tooltip("0-based column index for Z value in CSV.")]
    public int zCol = 5;

    void Start()
    {
        TextAsset csvData = Resources.Load<TextAsset>(fileName);
        if (csvData == null)
        {
            Debug.LogError($"CSVforMusee: Could not load TextAsset from Resources with key '{fileName}'. Put the CSV under Assets/Resources/ and set fileName without extension.");
            return;
        }

        if (pointerPrefab == null)
        {
            Debug.LogError("CSVforMusee: pointerPrefab is not set in the Inspector.");
            return;
        }

        // CSVファイルを読み込み、行ごとに分割する
        string[] lines = csvData.text.Split('\n');
        int spawned = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if(parts.Length < 8) continue; // フィールドが足りない行は無視

            string date = parts[1];
            string name = parts[2];
            string country = parts[3];
            string city = parts[4];

            // Safety: ignore lines that don't have enough columns for the configured indices
            int maxCol = Mathf.Max(xCol, yCol, zCol);
            if (parts.Length <= maxCol) continue;

            // Parse XYZ (try-parse to avoid hard crashes on bad lines)
            if (!float.TryParse(parts[xCol], out float rawX)) continue;
            if (!float.TryParse(parts[yCol], out float rawY)) continue;
            if (!float.TryParse(parts[zCol], out float rawZ)) continue;

            // Apply axis scaling (keep your floor plane as-is by leaving xScale/zScale=1)
            float x = rawX * xScale;
            float y = rawY * yScale;
            float z = rawZ * zScale;

            // Final position with optional offset
            Vector3 pos = new Vector3(x, y, z) + offset;

            // Spawn under parent if provided (and keep coordinates in that parent's local space)
            GameObject cityObject;
            if (spawnParent != null)
            {
                cityObject = Instantiate(pointerPrefab, spawnParent);
                cityObject.transform.localPosition = pos;
                cityObject.transform.localRotation = Quaternion.identity;
            }
            else
            {
                cityObject = Instantiate(pointerPrefab, pos, Quaternion.identity);
            }
            cityObject.name = name;
            PopUp popUpComponent = cityObject.GetComponent<PopUp>();
            if(popUpComponent != null)
            {
                popUpComponent.SetData($"{date}, {name},\n {country}, {city}");
            }
            spawned++;
        }
        Debug.Log($"CSVforMusee: Spawned {spawned} pointers from '{fileName}'" );
    }
}