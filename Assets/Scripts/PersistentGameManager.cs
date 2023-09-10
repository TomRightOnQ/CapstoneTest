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
            StartCoroutine(LoadAsync());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Load all prefab's info table async
    // Init Managers
    private IEnumerator LoadAsync()
    {
        PrefabConfig.Instance.ResetState();
        yield return StartCoroutine(PrefabConfig.Instance.LoadPrefabsAsync());
        PrefabManager.Instance.Init();
        GameEffectManager.Instance.Init();
        // SceneManager.LoadScene("MainMenu");
    }
}
