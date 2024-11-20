using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundFXSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
       if (PlayerPrefs.HasKey("masterVolume"))
        {
            loadVolume();
        }
    }

    public void SetMasterVolume(float volume) {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetSoundFXVolume(float volume) {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(volume) * 20f);

        PlayerPrefs.SetFloat("soundFXVolume", volume);
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void loadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        soundFXSlider.value = PlayerPrefs.GetFloat("soundFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
