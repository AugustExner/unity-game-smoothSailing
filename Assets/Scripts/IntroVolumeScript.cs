using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IntroVolumeScript : MonoBehaviour
{

    [SerializeField] private AudioSource musicObejct;

    void Start()
    {
        musicObejct.volume = PlayerPrefs.GetFloat("musicVolume");
    }

}
