using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All references of game prefabs
/// </summary>

[CreateAssetMenu(menuName = "Configs/PrefabConfig")]
public class PrefabConfig : ScriptableSingleton<PrefabConfig>
{
    [SerializeField]
    private List<PrefabData> prefabCollections = new List<PrefabData>();
    public List<PrefabData> PrefabCollections => prefabCollections;
}

[System.Serializable]
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
