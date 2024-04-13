using UnityEngine;
using System.Collections.Generic;

public class Line200Manager : MonoBehaviour
{
    public GameObject selectedPrefab; 
    public Material lineMaterial;

    private List<GameObject> equatorObjects = new List<GameObject>();
    private List<GameObject> latitude30Objects = new List<GameObject>();
    private List<GameObject> latitude60Objects = new List<GameObject>();
    private List<GameObject> latitude30SouthObjects = new List<GameObject>();
    private List<GameObject> latitude60SouthObjects = new List<GameObject>();
    private GameObject northpole;
    private GameObject southpole;

    public string filenameEquator = "Equator";
    public string filenameLatitude30 = "Latitude30Line"; 
    public string filenameLatitude60 = "Latitude60Line";
    public string filenameLatitude30South = "Latitude30LineSouth"; 
    public string filenameLatitude60South = "Latitude60LineSouth";

    void Start()
    {
        northpole = Instantiate(selectedPrefab, new Vector3(0, 200, 0), Quaternion.identity);
        southpole = Instantiate(selectedPrefab, new Vector3(0, -200, 0), Quaternion.identity);

        LoadAndInstantiateObjects(filenameEquator, equatorObjects);
        LoadAndInstantiateObjects(filenameLatitude30, latitude30Objects);
        LoadAndInstantiateObjects(filenameLatitude60, latitude60Objects);
        LoadAndInstantiateObjects(filenameLatitude30South, latitude30SouthObjects); 
        LoadAndInstantiateObjects(filenameLatitude60South, latitude60SouthObjects); 

        ConnectLatitudeObjects(equatorObjects);
        ConnectLatitudeObjects(latitude30Objects);
        ConnectLatitudeObjects(latitude60Objects);
        ConnectLatitudeObjects(latitude30SouthObjects);
        ConnectLatitudeObjects(latitude60SouthObjects);

        ConnectInterLatitudeObjects(equatorObjects, latitude30Objects);
        ConnectInterLatitudeObjects(latitude30Objects, latitude60Objects);
        ConnectInterLatitudeObjects(equatorObjects, latitude30SouthObjects);
        ConnectInterLatitudeObjects(latitude30SouthObjects, latitude60SouthObjects);

        ConnectObjectsWithNorthPole(latitude60Objects);
        ConnectObjectsWithSouthPole(latitude60SouthObjects);

        ConnectPoles();
    }

    void LoadAndInstantiateObjects(string filename, List<GameObject> objectList)
    {
        TextAsset csvData = Resources.Load<TextAsset>(filename);
        string[] lines = csvData.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length < 3) continue;

            float coord_x = float.Parse(parts[0]) * 200;
            float coord_y = float.Parse(parts[1]) * 200;
            float coord_z = float.Parse(parts[2]) * 200;

            GameObject marker = Instantiate(selectedPrefab, new Vector3(coord_x, coord_y, coord_z), Quaternion.identity);
            objectList.Add(marker);
        }
    }

    void ConnectLatitudeObjects(List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count - 1; i++)
        {
            CreateLine(objects[i], objects[i + 1]);
        }
        if (objects.Count > 0)
        {
            CreateLine(objects[objects.Count - 1], objects[0]);
        }
    }

    void ConnectInterLatitudeObjects(List<GameObject> lowerLatitude, List<GameObject> higherLatitude)
    {
        for (int i = 0; i < lowerLatitude.Count && i < higherLatitude.Count; i++)
        {
            CreateLine(lowerLatitude[i], higherLatitude[i]);
        }
    }

    void ConnectObjectsWithNorthPole(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            CreateLine(obj, northpole);
        }
    }

    void ConnectObjectsWithSouthPole(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            CreateLine(obj, southpole);
        }
    }

    void ConnectPoles()
    {
        CreateLine(northpole, southpole);
    }

    void CreateLine(GameObject start, GameObject end)
    {
        LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start.transform.position);
        lineRenderer.SetPosition(1, end.transform.position);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }
}
