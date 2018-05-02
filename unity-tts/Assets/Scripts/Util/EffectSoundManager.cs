using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectSoundType
{
    BlockAction,
}

public class EffectSoundManager : Singleton<EffectSoundManager>
{
    private Dictionary<EffectSoundType, AudioClip> effectMap = new Dictionary<EffectSoundType, AudioClip>();

    public EffectSoundManager()
    {
        LoadEffect();
    }

    private void LoadEffect()
    {
        var effects = Resources.LoadAll<AudioClip>("Sound/Effect");
        foreach (var effect in effects)
        {
            var type = (EffectSoundType)Enum.Parse(typeof(EffectSoundType), effect.name);
            effectMap[type] = effect;
        }
    }

    public void Play(EffectSoundType type)
    {
        AudioSource.PlayClipAtPoint(effectMap[type], Vector3.zero);
    }
}
