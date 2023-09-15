using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereObject : MEntity
{
    private float life = 0.5f;
    private float creationTime;

    public void Activate()
    {
        _activate();
    }

    public void Deactivate()
    {
        _deactivate();
    }


    private void _activate()
    {
        gameObject.SetActive(true);
    }

    // Dying

    private void _deactivate()
    {
        PrefabManager.Instance.Destroy(this.gameObject);
    }
}
