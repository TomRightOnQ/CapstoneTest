using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereObject : MonoBehaviour
{
    private float life = 0.5f;
    private float creationTime;

    public void OnEnable()
    {
        creationTime = Time.time; 
        Invoke("Deactivate", life);
    }

    public void OnDisable()
    {
        CancelInvoke("Deactivate");
        Deactivate();
    }

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
        gameObject.SetActive(false);
        Pooling.Instance.ReturnObj(this.gameObject);
    }
}
