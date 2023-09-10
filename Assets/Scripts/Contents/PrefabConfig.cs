using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// All references of game prefabs
/// </summary>

[CreateAssetMenu(menuName = "Configs/PrefabConfig")]
public class PrefabConfig : ScriptableSingleton<PrefabConfig>
{
    // Structures to map references around
    private Dictionary<string, PrefabData> prefabReferences = new Dictionary<string, PrefabData>();
    private Dictionary<string, string> prefabTypes = new Dictionary<string, string>();
    private List<PrefabData> prefabCollections = new List<PrefabData>();

    private bool isLoaded = false;
    [SerializeField] private const string FILE_NAME = "Prefabs.json";
    [SerializeField] private const string SUFFIX = "(Clone)";

    public bool IsLoaded => isLoaded;
    public Dictionary<string, PrefabData> PrefabReferences => prefabReferences;
    public List<PrefabData> PrefabCollections => prefabCollections;

    public struct PrefabData
    {
        public string name;
        public string PrefabPath;
        public string TypeName;
        public int Count;
        public bool IsExpandable;
        public float ExpandableRatio;
        public bool Poolable;
        public GameObject PrefabReference;
    }

    // Call this method at the start of your game to reset the state
    public void ResetState()
    {
        prefabReferences.Clear();
        prefabCollections.Clear();
        isLoaded = false;
    }

    // Async load all prefabs
    public IEnumerator LoadPrefabsAsync()
    {
        if (isLoaded)
        {
            Debug.LogWarning("PrefabConfig: Prefab references already initialized, no need to duplicate");
            yield break;
        }

        // Load and Parse
        string path = Path.Combine(Application.streamingAssetsPath, FILE_NAME);
        string json = File.ReadAllText(path);
        PrefabData[] Data = JsonConvert.DeserializeObject<PrefabData[]>(json);

        // Populate the list and load prefabs
        for (int i = 0; i < Data.Length; i++)
        {
            // Start an asynchronous load
            ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(Data[i].PrefabPath);
            yield return resourceRequest;

            GameObject loadedPrefab = resourceRequest.asset as GameObject;
            if (loadedPrefab != null)
            {
                Data[i].PrefabReference = loadedPrefab;
                prefabCollections.Add(Data[i]);
                prefabReferences[loadedPrefab.name] = Data[i];
                prefabTypes[loadedPrefab.name + SUFFIX] = Data[i].TypeName;
            }
            else
            {
                Debug.LogWarning("Failed to load prefab at path: " + Data[i].PrefabPath);
            }
        }
        isLoaded = true;
    }


    // Load all prefab from json file
    public void LoadPrefabsSync()
    {
        if (isLoaded)
        {
            Debug.LogWarning("PrefabConfig: Prefab references already initialized,no need to duplicate");
            return;
        }
        // Load and Parse
        string path = Path.Combine(Application.streamingAssetsPath, FILE_NAME);
        string json = File.ReadAllText(path);
        PrefabData[] Data = JsonConvert.DeserializeObject<PrefabData[]>(json);

        // Populate the list and load prefabs
        for (int i = 0; i < Data.Length; i++)
        {
            GameObject loadedPrefab = Resources.Load<GameObject>(Data[i].PrefabPath);
            if (loadedPrefab != null)
            {
                Data[i].PrefabReference = loadedPrefab;
                prefabCollections.Add(Data[i]);
                prefabReferences[loadedPrefab.name] = Data[i];
                prefabTypes[loadedPrefab.name + SUFFIX] = Data[i].TypeName;
            }
            else
            {
                Debug.LogWarning("Failed to load prefab at path: " + Data[i].PrefabPath);
            }
        }
        isLoaded = true;
    }

    // Init pools according to the list
    public void InitPoolings()
    {
        if (!isLoaded)
        {
            Debug.LogWarning("PrefabConfig: Prefab references not initialized!");
            return;
        }
        foreach (var prefabData in prefabCollections)
        {
            if (prefabData.Poolable)
            {
                PrefabManager.Instance.SetPoolUp(prefabData.TypeName, prefabData.PrefabReference, prefabData.Count, prefabData.IsExpandable, prefabData.ExpandableRatio);
            }
        }
    }

    public GameObject GetReference(string name)
    {
        return prefabReferences[name].PrefabReference;
    }

    public string GetReferenceType(string name)
    {
        return prefabReferences[name].TypeName;
    }

    public string GetReferenceType(GameObject prefab)
    {
        prefabTypes.TryGetValue(prefab.name, out string componentName);
        return componentName;
    }
}
