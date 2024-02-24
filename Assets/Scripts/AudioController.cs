using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioController : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds;

    public AudioSource Play(SoundType type, float volume = 1, bool loop = false)
    {
        var foundSound = sounds.FirstOrDefault(sound => sound.Type == type);
        if (foundSound.Clip == null || foundSound.Clip.Length == 0)
        {
            throw new ArgumentException("Could not find audioclip with given type");
        }

        AudioClip foundClip = foundSound.Clip[Random.Range(0, foundSound.Clip.Length)];

        GameObject createdObject = new GameObject("Audio Source");
        AudioSource createdSource = createdObject.AddComponent<AudioSource>();

        createdSource.clip = foundClip;
        createdSource.volume = volume;
        createdSource.pitch = Random.Range(0.95f, 1.05f);
        createdSource.loop = loop;
        
        createdSource.Play();
        
        return createdSource;
    }
}

[Serializable]
public class Sound
{
   [field: SerializeField] public SoundType Type { get; set; }
   [field: SerializeField] public AudioClip[] Clip { get; set; }
}

public enum SoundType
{
    ComputerBuzz
}