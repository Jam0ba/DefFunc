using System.Collections.Generic;
using UnityEngine;

public class SoundFXManagerEnemy : MonoBehaviour
{
    private AudioSource audioSource;


    public AudioClip[] soundClips;
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        foreach (var clip in soundClips)
        {
            if (clip != null)
            {
                soundEffects[clip.name] = clip;
            }
        }
    }


    public void PlaySound(string soundName)
    {
        if (audioSource != null)
        {

            if (soundEffects.TryGetValue(soundName, out AudioClip clip))
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }

    }


    public void StopSound()
    {
        if(audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }


    }
}
