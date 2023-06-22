using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public static AnimManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayAnim(string _name, Vector3 pos, Vector3 scale)
    {
        GameObject animObject = PrefabManager.Instance.GetReference("AnimObject");
        if (animObject == null || animObject.GetComponent<AnimObject>() == null)
        {
            Debug.LogWarning("Unable to accquire anim from pooling");
            return;
        }
        AnimObject anim = animObject.GetComponent<AnimObject>();
        anim.SetUp(_name, pos);
        animObject.transform.position = pos;
        animObject.transform.localRotation = Quaternion.Euler(45, 0, 0);
        animObject.transform.localScale = scale;
    }
}
