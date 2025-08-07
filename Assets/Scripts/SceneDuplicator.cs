#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class SceneDuplicator
{
    [MenuItem("Tools/Duplicate Equator Scenes")]
    public static void DuplicateEquatorScenes()
    {
        string baseScene = "Assets/Scenes/Equator/EquatorTemplate.unity";
        for (int i = 0; i < 8; i++)
        {
            string newScene = $"Assets/Scenes/Equator/Eq{i}.unity";
            File.Copy(baseScene, newScene, overwrite: true);
        }
        AssetDatabase.Refresh();
        Debug.Log("Equator Scenes duplicated.");
    }
}
#endif