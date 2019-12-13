using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SetVolumeValue : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume (float sliderValue)
    {
        audioMixer.SetFloat("ZeldaMusicVolume", Mathf.Log10 (sliderValue) * 20);
    }

    /*
    public void ToggleVolume()
    {
        audioMixer.SetFloat("ZeldaMusicVolume", 0);
    }
    */
    
}
