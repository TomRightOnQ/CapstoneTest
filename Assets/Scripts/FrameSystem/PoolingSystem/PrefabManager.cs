using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// This file is manages the references of all objects, communicating with the pooling system.
/// </summary>

public class PrefabManager : MonoBehaviour
{
    private static PrefabManager instance;
    public static PrefabManager Instance => instance;

    // Structures to map references around
    private Dictionary<string, PrefabData> prefabReferences = new Dictionary<string, PrefabData>();
    private Dictionary<string, string> prefabTypes = new Dictionary<string, string>();
    public Dictionary<string, PrefabData> PrefabReferences => prefabReferences;

    [SerializeField] private const string FILE_NAME = "Prefabs.json";
    [SerializeField] private const string SUFFIX = "(Clone)";

    private void Awake()
    {
        Debug.Log("PrefabManager Awake called");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Add to the pooling system
    public void SetPoolUp(string typeName, GameObject prefabReference, int count, bool isExpandable, float expandableRatio)
    {
        Type componentType = Type.GetType(typeName);
        if (componentType == null)
        {
            Debug.LogWarning("Failed to get the type for component: " + typeName);
            return;
        }
        MethodInfo createPoolMethod = typeof(Pooling).GetMethod("CreatePool");
        MethodInfo genericCreatePoolMethod = createPoolMethod.MakeGenericMethod(componentType);
        if (Pooling.Instance != null)
        {
            genericCreatePoolMethod.Invoke(Pooling.Instance, new object[] { prefabReference, count, isExpandable, expandableRatio });
        }
        else
        {
            Debug.LogWarning("Pooling.Instance is null");
        }
    }

    public void Init()
    {
        foreach (var prefabData in PrefabConfig.Instance.PrefabCollections)
        {
            if (prefabData.Poolable)
            {
                SetPoolUp(prefabData.TypeName, prefabData.PrefabReference, prefabData.Count, prefabData.IsExpandable, prefabData.ExpandableRatio);
                prefabReferences[prefabData.name] = prefabData;
                prefabTypes[prefabData.name + SUFFIX] = prefabData.TypeName;
            }
        }
    }

    // Call this method at the start of your game to reset the state
    public void ResetState()
    {
        prefabReferences.Clear();
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
