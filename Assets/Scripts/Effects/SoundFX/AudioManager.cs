using UnityEngine;

/// <summary>
/// Manage all SFX and sound clip playing
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

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

    // Play any desired clip by name
    public void PlaySound(string _name, Vector3 pos)
    {
        GameObject sfxObj = PrefabManager.Instance.GetReference("SFXObject");
        if (sfxObj == null || sfxObj.GetComponent<SFXObject>() == null)
        {
            Debug.LogWarning("Unable to accquire SFX from pooling");
            return;
        }
        SFXObject sfx = sfxObj.GetComponent<SFXObject>();
        sfx.transform.position = pos;
        sfx.SetUp(name, pos);
    }
}
