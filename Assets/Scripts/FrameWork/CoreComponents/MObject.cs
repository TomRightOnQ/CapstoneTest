using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Core base class
/// </summary>

public class MObject : MonoBehaviour
{
    // Unique ID as serialized private field
    [SerializeField, ReadOnly]
    private long uuid;
    public long UUID { get { return uuid; } private set { uuid = value; } }

    void Awake()
    {
        uuid = PersistentDataManager.Instance.GetMObjectID();
    }
}

