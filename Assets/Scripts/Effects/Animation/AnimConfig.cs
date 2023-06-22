using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimConfig : ScriptableObject
{
    private static AnimConfig instance = null;

    public static AnimConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<AnimConfig>(typeof(AnimConfig).Name);
                if (instance == null)
                {
                    instance = ScriptableObject.CreateInstance<AnimConfig>();
                    Debug.LogWarning("Created instance of " + typeof(AnimConfig).Name + " because none was found in resources.");
                }
            }
            return instance;
        }
    }

    // Define a serializable class to store the name and the anim clip
    [System.Serializable]
    public class NamedAnimClip
    {
        public string name;
        public AnimationClip animClip;
    }

    // Create an array of NamedAnimClip which you can populate in the Unity Editor
    [SerializeField]
    private NamedAnimClip[] animClips;

    // Dictionary to store anim clips by name
    private Dictionary<string, AnimationClip> animClipDictionary;

    public void Init()
    {
        animClipDictionary = new Dictionary<string, AnimationClip>();

        foreach (var namedClip in animClips)
        {
            animClipDictionary[namedClip.name] = namedClip.animClip;
        }
    }

    public AnimationClip GetClip(string name)
    {
        AnimationClip clip;
        animClipDictionary.TryGetValue(name, out clip);
        return clip;
    }
}
