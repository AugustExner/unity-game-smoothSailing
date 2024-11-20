using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private AudioSource soundFXObejct;

    private void Awake()
    {
        if (instance == null) {
        instance = this;
        }
    }


    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObejct, spawnTransform.position, Quaternion.identity);

        // Assign Audio Clip
        audioSource.clip = audioClip;

        // Assign Volume
        audioSource.volume = volume;

        // Play Sound
        audioSource.Play();

        // Get length of sound clip
        float clipLength = audioSource.clip.length;

        // Destroy the clip after playing
        Destroy(audioSource, clipLength);
    }
}
