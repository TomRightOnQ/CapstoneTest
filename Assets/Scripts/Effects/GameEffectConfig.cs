using System.Collections.Generic;
using UnityEngine;

public class GameEffectConfig<TClip> : ScriptableObject where TClip : Object
{
    private static GameEffectConfig<TClip> instance = null;

    public static GameEffectConfig<TClip> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameEffectConfig<TClip>>(typeof(GameEffectConfig<TClip>).Name);
                if (instance == null)
                {
                    instance = ScriptableObject.CreateInstance<GameEffectConfig<TClip>>();
                    Debug.LogWarning("Created instance of " + typeof(GameEffectConfig<TClip>).Name + " because none was found in resources.");
                }
            }
            return instance;
        }
    }

    [System.Serializable]
    public class NamedClip
    {
        public string name;
        public TClip clip;
    }

    [SerializeField]
    private NamedClip[] clips;

    private Dictionary<string, TClip> clipDictionary;

    public void Init()
    {
        clipDictionary = new Dictionary<string, TClip>();

        foreach (var namedClip in clips)
        {
            clipDictionary[namedClip.name] = namedClip.clip;
        }
    }

    public TClip GetClip(string name)
    {
        TClip clip;
        clipDictionary.TryGetValue(name, out clip);
        return clip;
    }
}
