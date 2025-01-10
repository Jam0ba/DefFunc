using System.Collections.Generic;
using UnityEngine;

public class SoundFXManagerEnemy : MonoBehaviour
{
    private AudioSource audioSource;

    // A dictionary to store sound effect names and their corresponding clips
    public AudioClip[] soundClips;
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    void Start()
    {
        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Populate the dictionary with sound clips
        foreach (var clip in soundClips)
        {
            if (clip != null)
            {
                soundEffects[clip.name] = clip;
            }
        }
    }

    // Public method to play a sound by name
    public void PlaySound(string soundName)
    {
        if (soundEffects.TryGetValue(soundName, out AudioClip clip))
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found in SoundFXManagerPlayer!");
        }
    }

    // Optional: Stop any currently playing sound
    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
