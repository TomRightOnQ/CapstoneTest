using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reference of all audio clips compiled in game
/// SFX and short clips
/// </summary>
/// 
[CreateAssetMenu(menuName = "Audio/AudioConfig")]
public class AudioConfig : ScriptableObject
{
    private static AudioConfig instance = null;

    public static AudioConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<AudioConfig>(typeof(AudioConfig).Name);
                if (instance == null)
                {
                    instance = ScriptableObject.CreateInstance<AudioConfig>();
                    Debug.LogWarning("Created instance of " + typeof(AudioConfig).Name + " because none was found in resources.");
                }
            }
            return instance;
        }
    }

    // Define a serializable class to store the name and the audio clip
    [System.Serializable]
    public class NamedAudioClip
    {
        public string name;
        public AudioClip audioClip;
    }

    // Create an array of NamedAudioClip which you can populate in the Unity Editor
    [SerializeField]
    private NamedAudioClip[] audioClips;

    // Dictionary to store audio clips by name
    private Dictionary<string, AudioClip> audioClipDictionary;

    // Initialize the audioClipDictionary from the array
    public void Init()
    {
        audioClipDictionary = new Dictionary<string, AudioClip>();

        foreach (var namedClip in audioClips)
        {
            audioClipDictionary[namedClip.name] = namedClip.audioClip;
        }
    }

    // Method to get audio clip by name
    public AudioClip GetClip(string name)
    {
        AudioClip clip;
        audioClipDictionary.TryGetValue(name, out clip);
        return clip;
    }
}
