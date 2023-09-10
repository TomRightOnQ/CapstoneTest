using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// Core Object lives through the entire game run-time
/// Manager the game session's manager init
/// </summary>
 
public class PersistentGameManager : MonoBehaviour
{
    private static PersistentGameManager instance;
    public static PersistentGameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            Load();
            //StartCoroutine(LoadAsync());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Init Managers
    private void Load()
    {
        Debug.Log("PrefabManager Loading");
        PrefabManager.Instance.Init();
        GameEffectManager.Instance.Init();
        // SceneManager.LoadScene("MainMenu");
    }
}
