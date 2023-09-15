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

    private static bool bConfigReady = false;

    private void Awake()
    {
        gameObject.tag = "Manager";
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            if (!bConfigReady)
            {
                bConfigReady = true;
                LoadConfigs();
            }
            LoadManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Init Configs
    private void LoadConfigs() 
    {
        // Configs
        Debug.Log("1. PrefabConfig Loading");
        PrefabConfig.Instance.Init();
    }

    // Init Managers
    private void LoadManagers()
    {
        // Managers
        Debug.Log("1. PrefabManager Loading");
        PrefabManager.Instance.Init();

        Debug.Log("2. GameEffectManager Loading");
        GameEffectManager.Instance.Init();

        Debug.Log("PersistentGameManager: Ready!");
        // SceneManager.LoadScene("MainMenu");
    }
}
