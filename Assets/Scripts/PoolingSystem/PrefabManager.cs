using System;
using System.Reflection;
using UnityEngine;
using static PrefabConfig;

/// <summary>
/// This file is manages the references of all objects, communicating with the pooling system.
/// </summary>

public class PrefabManager : MonoBehaviour
{
    private static PrefabManager instance;
    public static PrefabManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        PrefabConfig.Instance.ResetState();
        PrefabConfig.Instance.LoadPrefabs();
        InitPoolings();
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

    public void InitPoolings()
    {
        PrefabConfig.Instance.InitPoolings();
    }

    // Get a reference via name
    public GameObject GetReference(string name)
    {
        GameObject reference = PrefabConfig.Instance.GetReference(name);
        if (reference == null)
        {
            Debug.Log("Missing reference of " + name + " in PrefabConfig");
        }
        return reference;
    }

    public string GetReferenceType(string name)
    {
        string reference = PrefabConfig.Instance.GetReferenceType(name);
        if (reference == null)
        {
            Debug.Log("Missing reference of " + name + " in PrefabConfig");
        }
        return reference;
    }

    public string GetReferenceType(GameObject prefab)
    {
        string reference = PrefabConfig.Instance.GetReferenceType(prefab);
        if (reference == null)
        {
            Debug.Log("Missing reference of " + prefab.name + " in PrefabConfig");
        }
        return reference;
    }
}
