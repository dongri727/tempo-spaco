using UnityEngine;
using System.Collections.Generic; //Required for making List.

public class GlobeSelectedManager : MonoBehaviour
{
    
    // Specifies Prefab.
    public GameObject selectedPrefab; 
    public Material lineMaterial;

    private List<GameObject> spawnedObjects = new List<GameObject>();
   
    // CSV file's name.
    public string fileName = "SelectedGlobe"; 


    void Start ()
    {
        // Reads CSV file and split by row.
        TextAsset csvData = Resources.Load<TextAsset>(fileName);
        string[] lines = csvData.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if(parts.Length < 5) continue;

            float coord_x = float.Parse(parts[0])/25;
            float coord_y = float.Parse(parts[1])/25;
            float coord_z = float.Parse(parts[2])/25;
            //float coefficient = parts[3];

            //Creates object and sets position.
            GameObject selectedMarker = Instantiate(selectedPrefab, new Vector3(coord_x, coord_y, coord_z), Quaternion.identity);
            spawnedObjects.Add(selectedMarker);
        }

        DrawLinesBetweenObjects();
    }

    void DrawLinesBetweenObjects()
    {
        for (int i = 0; i < spawnedObjects.Count - 1; i++)
        {
            GameObject startObject = spawnedObjects[i];
            GameObject endObject = spawnedObjects[i + 1];

            LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startObject.transform.position);
            lineRenderer.SetPosition(1, endObject.transform.position);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

        }
    }
}
